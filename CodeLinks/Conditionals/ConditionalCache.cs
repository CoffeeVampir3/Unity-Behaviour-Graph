using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;

namespace BehaviourGraph.Conditionals
{
    public static class ConditionalCache
    {
        private static bool initialized = false;
        private static Dictionary<(Type, Type), MethodInfo[]> conditionalMethods;
        private static Dictionary<(Type, Type), FieldInfo[]> conditionalFields;
        private static List<MemberInfo[]> conditionalMembers;
        private static List<Type> classesWithConditions;

        private static void InitializeCache()
        {
            if (initialized)
                return;
            
            initialized = true;
            classesWithConditions = new List<Type>();
            
            var fieldsList = TypeCache.GetFieldsWithAttribute<Condition>().ToArray();
            var methodsList = TypeCache.GetMethodsWithAttribute<Condition>().ToArray();
            
            var allFields = AttributeCacheFactory.CacheMemberInfo<FieldInfo, Condition>(
                ref conditionalFields, 
                ref classesWithConditions,
                ref fieldsList);
            
            var allMethods = AttributeCacheFactory.CacheMemberInfo<MethodInfo, Condition>(
                ref conditionalMethods, 
                ref classesWithConditions,
                ref methodsList);

            conditionalMembers = new List<MemberInfo[]>();
            conditionalMembers.Add(allFields);
            conditionalMembers.Add(allMethods);

            GetCachedMemberDropdown();
        }

        public static bool GetCachedMemberViaLookupValue(string value, out MemberInfo memberInfo)
        {
            InitializeCache();
            if(dropdownLookup.TryGetValue(value, out memberInfo))
            {
                return true;
            }
            return false;
        }

        private static ValueDropdownList<string> cachedValueDropdown;
        private static Dictionary<string, MemberInfo> dropdownLookup;
        public static ValueDropdownList<string> GetCachedMemberDropdown()
        {
            if (cachedValueDropdown != null)
                return cachedValueDropdown;
            
            cachedValueDropdown = new ValueDropdownList<string>();
            dropdownLookup = new Dictionary<string, MemberInfo>();
            var members = GetConditionalMemberList();

            for(int j = 0; j < members.Count; j++)
            {
                var member = members[j];
                for (int i = 0; i < member.Length; i++)
                {
                    string mew = member[i].ReflectedType.Name + "/" + member[i].Name;
                    cachedValueDropdown.Add(mew, mew);
                    dropdownLookup.Add(mew, member[i]);
                }
            }

            return cachedValueDropdown;
        }

        public static List<MemberInfo[]> GetConditionalMemberList()
        {
            InitializeCache();
            return conditionalMembers;
        }

    }
}
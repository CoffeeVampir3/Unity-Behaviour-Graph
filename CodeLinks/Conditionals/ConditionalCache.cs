using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BehaviourGraph.CodeLinks;
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

            var allFields = AttributeCacheRetainer.
                CacheOrGetCachedAttributeData<FieldInfo, Condition>(
                    out conditionalFields
            );

            var allMethods = AttributeCacheRetainer.
                CacheOrGetCachedAttributeData<MethodInfo, Condition>(
                    out conditionalMethods
                );

            conditionalMembers = new List<MemberInfo[]> {allFields, allMethods};

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
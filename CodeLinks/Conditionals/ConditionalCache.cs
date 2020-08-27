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
        private static void InitializeCache()
        {
            if (initialized)
                return;
            
            initialized = true;
            classesWithConditions = new List<Type>();
            
            tempConditionalMembers = new Dictionary<(Type, Type), List<MemberInfo>>();
            AttributeCacheFactory.CacheMemberInfo<FieldInfo, Condition>(
                ref conditionalFields, 
                TypeCache.GetFieldsWithAttribute<Condition>().ToArray(),
                AddNameToClassList);
            
            AttributeCacheFactory.CacheMemberInfo<MethodInfo, Condition>(
                ref conditionalMethods, 
                TypeCache.GetMethodsWithAttribute<Condition>().ToArray(),
                AddNameToClassList);

            conditionalMembers = new List<MemberInfo[]>();
            foreach (Type t in classesWithConditions)
            {
                var index = (t, typeof(Condition));
                if (tempConditionalMembers.TryGetValue(index, out var members))
                {
                    conditionalMembers.Add(members.ToArray());
                }
            }

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
            var members = ConditionalCache.GetConditionalMemberList();

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
        
        public static bool TryGetConditionsFor(Type type, out MethodInfo[] outItem)
        {
            InitializeCache();
            return conditionalMethods.TryGetValue((type, typeof(Condition)), out outItem);
        }
        
        public static bool TryGetConditionsFor(Type type, out FieldInfo[] outItem)
        {
            InitializeCache();
            return conditionalFields.TryGetValue((type, typeof(Condition)), out outItem);
        }

        private static Dictionary<(Type, Type), MethodInfo[]> conditionalMethods;
        private static Dictionary<(Type, Type), FieldInfo[]> conditionalFields;
        private static List<MemberInfo[]> conditionalMembers;

        private static List<Type> classesWithConditions;

        public static List<Type> ClassesWithCondition
        {
            get
            {
                InitializeCache();
                return classesWithConditions;
            }
        }

        private static Dictionary<(Type, Type), List<MemberInfo>> tempConditionalMembers;
        private static void AddNameToClassList(MemberInfo item)
        {
            if (!classesWithConditions.Contains(item.DeclaringType))
            {
                classesWithConditions.Add(item.DeclaringType);
            }

            var index = (item.DeclaringType, typeof(Condition));
            if(tempConditionalMembers.TryGetValue(index, out var mList))
            {
                mList.Add(item);
            }
            else
            {
                List<MemberInfo> members = new List<MemberInfo>();
                members.Add(item);
                tempConditionalMembers.Add(index, members);
            }
        }

    }
}
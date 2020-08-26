using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Attribute = System.Attribute;

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
            
            CacheMemberInfo<FieldInfo, Condition>(ref conditionalFields, 
                TypeCache.GetFieldsWithAttribute<Condition>().ToArray());
            CacheMemberInfo<MethodInfo, Condition>(ref conditionalMethods, 
                TypeCache.GetMethodsWithAttribute<Condition>().ToArray());
        }
        
        public static bool TryGetCondition(Type type, out MethodInfo[] outItem)
        {
            InitializeCache();
            return conditionalMethods.TryGetValue((type, typeof(Condition)), out outItem);
        }
        
        public static bool TryGetCondition(Type type, out FieldInfo[] outItem)
        {
            InitializeCache();
            return conditionalFields.TryGetValue((type, typeof(Condition)), out outItem);
        }

        private static Dictionary<(Type, Type), MethodInfo[]> conditionalMethods;
        private static Dictionary<(Type, Type), FieldInfo[]> conditionalFields;

        private static List<Type> classesWithConditions;

        public static List<Type> ClassesWithCondition
        {
            get
            {
                InitializeCache();
                return classesWithConditions;
            }
        }

        private static void AddNameToClassList(Type decType)
        {
            if (!classesWithConditions.Contains(decType))
            {
                classesWithConditions.Add(decType);
            }
        }
        
        private static void CacheMemberInfo<CachingItem, Attr>(
            ref Dictionary<(Type, Type), CachingItem[]> cacheDictionary, CachingItem[] itemSelection) 
            where CachingItem : MemberInfo 
            where Attr : Attribute
        {
            cacheDictionary = new Dictionary<(Type, Type), CachingItem[]>();

            foreach (CachingItem item in itemSelection)
            {
                var dictionaryIndex = (item.DeclaringType, typeof(Attr));
                if (!cacheDictionary.TryGetValue(dictionaryIndex, out var info))
                {
                    info = new CachingItem[1];
                }
                else
                {
                    var temp = info;
                    info = new CachingItem[info.Length + 1];
                    temp.CopyTo(info, 0);
                }
                
                info[info.Length - 1] = item;
                cacheDictionary.Remove(dictionaryIndex);
                cacheDictionary.Add(dictionaryIndex, info);

                AddNameToClassList(item.DeclaringType);
            }
        }
        
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            
            AttributeCacheFactory.CacheMemberInfo<FieldInfo, Condition>(
                ref conditionalFields, 
                TypeCache.GetFieldsWithAttribute<Condition>().ToArray(),
                AddNameToClassList);
            
            AttributeCacheFactory.CacheMemberInfo<MethodInfo, Condition>(
                ref conditionalMethods, 
                TypeCache.GetMethodsWithAttribute<Condition>().ToArray(),
                AddNameToClassList);
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

        private static void AddNameToClassList(MemberInfo item)
        {
            if (!classesWithConditions.Contains(item.DeclaringType))
            {
                classesWithConditions.Add(item.DeclaringType);
            }
        }

    }
}
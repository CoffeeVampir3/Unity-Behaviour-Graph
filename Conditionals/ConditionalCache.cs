using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.Conditionals
{
    public static class ConditionalCache
    {
        public static bool TryGetCondition(Type type, out MethodInfo[] outItem)
        {
            if(conditionalMethods == null)
                CacheMethods();
            return conditionalMethods.TryGetValue(type, out outItem);
        }
        
        public static bool TryGetCondition(Type type, out FieldInfo[] outItem)
        {
            if(conditionalFields == null)
                CacheFields();
            return conditionalFields.TryGetValue(type, out outItem);
        }
        
        private static Dictionary<Type, MethodInfo[]> conditionalMethods;
        private static Dictionary<Type, FieldInfo[]> conditionalFields;

        private static void CacheMethods()
        {
            conditionalMethods = new Dictionary<Type, MethodInfo[]>();
            foreach (var m in TypeCache.GetMethodsWithAttribute<Condition>())
            {
                if (!conditionalMethods.TryGetValue(m.DeclaringType, out var info))
                {
                    info = new MethodInfo[1];
                }
                else
                {
                    var temp = info;
                    info = new MethodInfo[info.Length + 1];
                    temp.CopyTo(info, 0);
                }
                Debug.Log(info.Length - 1);
                info[info.Length - 1] = m;
                conditionalMethods.Remove(m.DeclaringType);
                conditionalMethods.Add(m.DeclaringType, info);
            }
        }

        private static void CacheFields()
        {
            conditionalFields = new Dictionary<Type, FieldInfo[]>();
            foreach (var t in TypeCache.GetFieldsWithAttribute<Condition>())
            {
                if (!conditionalFields.TryGetValue(t.DeclaringType, out var info))
                {
                    info = new FieldInfo[1];
                }
                else
                {
                    var temp = info;
                    info = new FieldInfo[info.Length + 1];
                    temp.CopyTo(info, 0);
                }
                info[info.Length - 1] = t;
                conditionalFields.Remove(t.DeclaringType);
                conditionalFields.Add(t.DeclaringType, info);
            }
        }
    }
}
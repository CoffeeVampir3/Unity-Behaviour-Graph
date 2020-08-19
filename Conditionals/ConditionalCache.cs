using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;

namespace BehaviourGraph.Conditionals
{
    public static class ConditionalCache
    {
        private static bool initialized = false;
        public static void InitializeCache()
        {
            if (initialized)
                return;
            
            initialized = true;
            classesWithConditions = new List<Type>();
            CacheFields();
            CacheMethods();
        }
        
        public static bool TryGetCondition(Type type, out MethodInfo[] outItem)
        {
            return conditionalMethods.TryGetValue(type, out outItem);
        }
        
        public static bool TryGetCondition(Type type, out FieldInfo[] outItem)
        {
            return conditionalFields.TryGetValue(type, out outItem);
        }
        
        private static Dictionary<Type, MethodInfo[]> conditionalMethods;
        private static Dictionary<Type, FieldInfo[]> conditionalFields;
        
        private static List<Type> classesWithConditions;

        private static void AddNameToClassList(Type decType)
        {
            if (!classesWithConditions.Contains(decType))
            {
                classesWithConditions.Add(decType);
            }
        }
        
        private static ValueDropdownList<Type> filterList;
        public static ValueDropdownList<Type> GetDropdownListOfClassesWithConditions() {
            InitializeCache();

            if (filterList == null)
            {
                filterList = new ValueDropdownList<Type>();
                var typeList = classesWithConditions;
                foreach (var listedType in typeList) {
                    filterList.Add(listedType.Name, listedType);
                }
                filterList.Sort((val1, val2) => String.Compare(val1.Text, val2.Text, StringComparison.Ordinal));
            }
            return filterList;
        }

        //TODO:: Potentially very slow/high cost array re-allocations in big projects.
        //Ideally we'd use a dictionary of lists first while allocating,
        //then finalize the method into arrays.
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
                info[info.Length - 1] = m;
                conditionalMethods.Remove(m.DeclaringType);
                conditionalMethods.Add(m.DeclaringType, info);

                AddNameToClassList(m.DeclaringType);
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

                AddNameToClassList(t.DeclaringType);
            }
        }
    }
}
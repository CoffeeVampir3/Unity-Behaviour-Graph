using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.Services
{
    public class ServiceCache
    {
        private static bool initialized = false;
        #if UNITY_EDITOR
        private static void InitializeCache()
        {
            if (initialized)
                return;
            
            initialized = true;
            classesWithServices = new List<Type>();

            var methodInfo = TypeCache.GetMethodsWithAttribute<Service>().ToArray();
            AttributeCacheFactory.CacheMemberInfo<MethodInfo, Service>(
                ref serviceMethods, 
                ref methodInfo,
                AddNameToClassList);
        }
        #else 
        private static void InitializeCache()
        {
        }
        #endif

        private static ValueDropdownList<MethodInfo> cachedServiceList;
        public static ValueDropdownList<MethodInfo> GetListOfServices()
        {
            if (cachedServiceList != null)
                return cachedServiceList;

            InitializeCache();
            cachedServiceList = new ValueDropdownList<MethodInfo>();
            MethodInfo[] methods;
            foreach (Type t in classesWithServices)
            {
                if (TryGetServicesFor(t, out methods))
                {
                    foreach (var m in methods)
                    {
                        cachedServiceList.Add(m.DeclaringType.Name + "/" + m.Name, m);
                    }
                }
            }
            return cachedServiceList;
        }
        
        public static bool TryGetServicesFor(Type type, out MethodInfo[] outItem)
        {
            InitializeCache();
            return serviceMethods.TryGetValue((type, typeof(Service)), out outItem);
        }
        
        private static Dictionary<(Type, Type), MethodInfo[]> serviceMethods;
        
        private static List<Type> classesWithServices;
        public static List<Type> ClassesWithServices
        {
            get
            {
                InitializeCache();
                return classesWithServices;
            }
        }
        
        private static void AddNameToClassList(MemberInfo item)
        {
            if (!classesWithServices.Contains(item.DeclaringType))
            {
                classesWithServices.Add(item.DeclaringType);
            }
        }
    }
}
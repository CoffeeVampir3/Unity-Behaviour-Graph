using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BehaviourGraph.CodeLinks;
using Sirenix.OdinInspector;
using UnityEditor;

namespace BehaviourGraph.Services
{
    public class ServiceCache
    {
        private static bool initialized = false;
        
        private static Dictionary<(Type, Type), MethodInfo[]> serviceMethods;
        private static MethodInfo[] serviceMethodInfo;

        private static void InitializeCache()
        {
            if (initialized)
                return;
            
            initialized = true;
            
            serviceMethodInfo = AttributeCacheRetainer.
                CacheOrGetCachedAttributeData<MethodInfo, Service>(
                    out serviceMethods
                );
        }

        private static ValueDropdownList<MethodInfo> cachedServiceList;
        public static ValueDropdownList<MethodInfo> GetListOfServices()
        {
            if (cachedServiceList != null)
                return cachedServiceList;

            InitializeCache();
            cachedServiceList = new ValueDropdownList<MethodInfo>();
            foreach (var method in serviceMethodInfo)
            {
                cachedServiceList.Add(method.DeclaringType.Name + "/" + method.Name, method);
            }
            return cachedServiceList;
        }
        
        public static bool TryGetServicesFor(Type type, out MethodInfo[] outItem)
        {
            InitializeCache();
            return serviceMethods.TryGetValue((type, typeof(Service)), out outItem);
        }
    }
}
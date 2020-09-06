using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.Serialization;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    [Serializable]
    public class MethodInfoStore
    {
        [OdinSerialize]
        private Dictionary<Type, List<MethodInfo>> storedFieldInfo = new Dictionary<Type, List<MethodInfo>>();

        public void Store<T>(List<MethodInfo> info)
            where T : Attribute
        {
            var infoType = typeof(T);

            storedFieldInfo.Remove(infoType);
            storedFieldInfo.Add(infoType, info);
        }

        internal List<MethodInfo> GetRuntimeMethodData<T>()
        {
            return storedFieldInfo.TryGetValue(typeof(T), out var info) ? info : null;
        }
    }
}
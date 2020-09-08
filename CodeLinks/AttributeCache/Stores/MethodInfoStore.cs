using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.Serialization;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    [Serializable]
    internal class MethodInfoStore : IMemberStore
    {
        [OdinSerialize]
        private Dictionary<Type, List<MethodInfo>> storedFieldInfo = 
            new Dictionary<Type, List<MethodInfo>>();
        [OdinSerialize]
        private Dictionary<int, MethodInfo> hashedMethods = 
            new Dictionary<int, MethodInfo>();
        
        public void Store<T>(List<MethodInfo> info, MemberLookupStore memberLookup)
            where T : Attribute
        {
            var infoType = typeof(T);
            foreach (var method in info)
            {
                hashedMethods.Add(method.GetHashCode(), method);
                memberLookup.Add(method, this);
            }

            storedFieldInfo.Remove(infoType);
            storedFieldInfo.Add(infoType, info);
        }

        internal List<MethodInfo> GetRuntimeMethodData<T>()
        {
            return storedFieldInfo.
                TryGetValue(typeof(T), out var info) 
                ? info : null;
        }

        public MemberInfo GetMemberByHash(int hash)
        {
            return hashedMethods.TryGetValue(hash, out var member) 
                ? member : null;
        }
    }
}
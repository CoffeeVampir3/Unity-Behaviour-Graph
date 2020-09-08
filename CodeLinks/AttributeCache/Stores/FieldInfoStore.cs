using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.Serialization;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    [Serializable]
    internal class FieldInfoStore : IMemberStore
    {
        [OdinSerialize]
        private Dictionary<Type, List<SerializedFieldInfo>> storedFieldInfo = new Dictionary<Type, List<SerializedFieldInfo>>();
        [OdinSerialize]
        private Dictionary<int, SerializedFieldInfo> hashedFieldInfo = new Dictionary<int, SerializedFieldInfo>();
        
        [Serializable]
        private struct SerializedFieldInfo
        {
            [OdinSerialize]
            public string fieldName;
            [OdinSerialize]
            public Type declaringType;

            public SerializedFieldInfo(string name, Type dType)
            {
                fieldName = name;
                declaringType = dType;
            }

            public FieldInfo FieldFromInfo => declaringType.GetField(fieldName);
        }

        public void Store<T>(List<FieldInfo> info, MemberLookupStore memberLookup)
            where T : Attribute
        {
            var infoType = typeof(T);
            List<SerializedFieldInfo> serializedInfo = new List<SerializedFieldInfo>();
            foreach (var field in info)
            {
                var recoverableInfo = new SerializedFieldInfo(
                    field.Name,
                    field.DeclaringType);
                
                serializedInfo.Add(recoverableInfo);
                hashedFieldInfo.Add(field.GetHashCode(), recoverableInfo);
                memberLookup.Add(field, this);
            }
            
            storedFieldInfo.Remove(infoType);
            storedFieldInfo.Add(infoType, serializedInfo);
        }

        internal List<FieldInfo> GetRuntimeFieldData<T>()
        {
            if (storedFieldInfo.TryGetValue(typeof(T), out var info))
            {
                List<FieldInfo> fields = new List<FieldInfo>(info.Count);
                foreach(var item in info)
                {
                    fields.Add(item.FieldFromInfo);
                }
                return fields;
            }
            
            return null;
        }

        public MemberInfo GetMemberByHash(int hash)
        {
            return hashedFieldInfo.
                TryGetValue(hash, out var info) 
                ? info.FieldFromInfo : null;
        }
    }
}
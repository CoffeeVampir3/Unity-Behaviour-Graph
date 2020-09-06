using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.Serialization;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    [Serializable]
    public class FieldInfoStore
    {
        [OdinSerialize]
        private Dictionary<Type, List<SerializedFieldInfo>> storedFieldInfo = new Dictionary<Type, List<SerializedFieldInfo>>();
        
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
        }

        public void Store<T>(List<FieldInfo> info)
            where T : Attribute
        {
            var infoType = typeof(T);
            List<SerializedFieldInfo> serializedInfo = new List<SerializedFieldInfo>();
            foreach (var field in info)
            {
                serializedInfo.Add(new SerializedFieldInfo(
                        field.Name, 
                        field.DeclaringType)
                );
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
                    fields.Add(item.declaringType.GetField(item.fieldName));
                }
                return fields;
            }
            
            return null;
        }
    }
}
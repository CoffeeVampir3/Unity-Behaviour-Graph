using System;
using System.Reflection;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph
{
    public class FieldAttributeStore : AttributeStore
    {
        [SerializeField]
        private string[] fieldNames;
        [NonSerialized, OdinSerialize] 
        private Type[] fieldDeclTypes;
        
        protected override void OnCreated(ref MemberInfo[] members)
        {
            if (!(members.Length > 0))
            {
                return;
            }

            if (!(members[0] is FieldInfo))
            {
                throw new InvalidCastException("Attempted to store a non-field in a field attribute cache.");
            }
            
            Store(ref members);
        }

        private void Store(ref MemberInfo[] info)
        {
            fieldNames = new string[info.Length];
            fieldDeclTypes = new Type[info.Length];
            for (int i = 0; i < info.Length; i++)
            {
                fieldNames[i] = info[i].Name;
                fieldDeclTypes[i] = info[i].DeclaringType;
            }
        }
        
        private FieldInfo[] GetRuntimeFieldData()
        {
            FieldInfo[] fields = new FieldInfo[fieldNames.Length];
            for (int i = 0; i < fieldNames.Length; i++)
            {
                fields[i] = fieldDeclTypes[i].GetField(fieldNames[i]);
            }
            return fields;
        }

        public override MemberInfo[] Retrieve()
        {
            return GetRuntimeFieldData();
        }

        public FieldInfo[] RetreiveAsField()
        {
            return GetRuntimeFieldData();
        }
        
    }
}
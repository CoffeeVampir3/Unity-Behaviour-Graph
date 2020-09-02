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

        public static FieldAttributeStore CreateOrStore<CachingItem, Attr>(ref CachingItem[] members) 
            where CachingItem : MemberInfo
            where Attr : Attribute
        {
            return CreateOrStore<FieldAttributeStore, CachingItem, Attr>(ref members);
        }

        protected override void OnCreated<CachingItem>(ref CachingItem[] members)
        {
            if (!(members.Length > 0))
            {
                return;
            }

            if (!(members[0] is FieldInfo))
            {
                throw new InvalidCastException("Attempted to store a non-field in a field attribute cache.");
            }
            
            FieldInfo[] fields = new FieldInfo[members.Length];
            members.CopyTo(fields, 0);
            Store(ref fields);
        }

        private void Store(ref FieldInfo[] info)
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
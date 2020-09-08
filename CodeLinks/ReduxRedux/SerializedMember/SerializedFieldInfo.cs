using System;
using System.Reflection;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    [Serializable]
    public class SerializedFieldInfo : ISerializedMemberInfo
    {
        [OdinSerialize]
        private string fieldName;
        [OdinSerialize]
        private Type declaringType;
        
        public FieldInfo FieldFromInfo => declaringType.GetField(fieldName);

        #region Constructors
        public SerializedFieldInfo(FieldInfo info)
        {
            fieldName = info.Name;
            declaringType = info.DeclaringType;
        }
        
        public SerializedFieldInfo(MemberInfo info)
        {
            fieldName = info.Name;
            declaringType = info.DeclaringType;
        }
        #endregion

        #region ISerializedMemberInfo impl
        
        public MemberInfo Get()
        {
            return FieldFromInfo;
        }

        public void Set(MemberInfo item)
        {
            Debug.Assert(item is FieldInfo);
            fieldName = item.Name;
            declaringType = item.DeclaringType;
        }
        
        #endregion
        
    }
}
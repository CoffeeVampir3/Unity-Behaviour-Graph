using System;
using System.Reflection;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    [Serializable]
    public class SerializedMethodInfo : ISerializedMemberInfo
    {
        [OdinSerialize]
        private MethodInfo methodInfo = null;
        
        public MethodInfo MethodFromInfo => methodInfo;
        
        #region constructors

        public SerializedMethodInfo(MethodInfo info)
        {
            methodInfo = info;
        }

        public SerializedMethodInfo(MemberInfo info)
        {
            Debug.Assert(info is MethodInfo);
            methodInfo = (MethodInfo) info;
        }
        
        #endregion
        
        #region ISerializedMemberInfo impl
        
        public MemberInfo Get()
        {
            return methodInfo;
        }

        public void Set(MemberInfo item)
        {
            if (item is MethodInfo methodItem)
            {
                methodInfo = methodItem;
            }
            else
            {
                Debug.Assert(item != methodInfo);
            }
        }
        
        #endregion
    }
}
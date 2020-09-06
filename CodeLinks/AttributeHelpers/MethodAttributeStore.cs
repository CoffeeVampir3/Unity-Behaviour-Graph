using System;
using System.Reflection;
using Sirenix.Serialization;

namespace BehaviourGraph
{
    public class MethodAttributeStore : AttributeStore
    {
        [OdinSerialize]
        private MethodInfo[] methods;
        
        #if UNITY_EDITOR
        public static MethodAttributeStore CreateOrStore<CachingItem, Attr>(ref CachingItem[] members) 
            where CachingItem : MemberInfo
            where Attr : Attribute
        {
            return CreateOrStore<MethodAttributeStore, CachingItem, Attr>(ref members);
        }
        #endif
        
        protected override void OnCreated<CachingItem>(ref CachingItem[] members)
        {
            if (!(members.Length > 0))
            {
                return;
            }

            if (!(members[0] is MethodInfo))
            {
                throw new InvalidCastException("Attempted to store a non-method in a method attribute cache.");
            }

            methods = new MethodInfo[members.Length];
            members.CopyTo(methods, 0);
        }
        
        public override MemberInfo[] Retrieve()
        {
            return methods;
        }
        
        public MethodInfo[] RetrieveAsMethod()
        {
            return methods;
        }
    }
}
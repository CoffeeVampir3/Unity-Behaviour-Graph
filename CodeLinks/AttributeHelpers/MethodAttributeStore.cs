using System;
using System.Reflection;
using Sirenix.Serialization;

namespace BehaviourGraph
{
    public class MethodAttributeStore : AttributeStore
    {
        [OdinSerialize]
        private MethodInfo[] methods;
        protected override void OnCreated(ref MemberInfo[] members)
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
        
        public MethodInfo[] RetreiveAsMethod()
        {
            return methods;
        }
    }
}
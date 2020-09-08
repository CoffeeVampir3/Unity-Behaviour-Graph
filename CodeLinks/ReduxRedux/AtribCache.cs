using System;
using Sirenix.OdinInspector;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    public static class AtribCache <Attr>
        where Attr : Attribute
    {
        private static ValueDropdownList<string> cachedValueDropdown;
        public static ValueDropdownList<string> GetCachedMemberDropdown()
        {
            if (cachedValueDropdown != null)
                return cachedValueDropdown;
            
            cachedValueDropdown = new ValueDropdownList<string>();
            
            if (SerializedMemberStore.TryGetMembersByAttribute<Attr>(out var members))
            {
                foreach (var member in members)
                {
                    string memberReflectName = SerializedMemberStore.MemberToString(member);
                    cachedValueDropdown.Add(memberReflectName, memberReflectName);
                }
            }
            return cachedValueDropdown;
        }
    }
}
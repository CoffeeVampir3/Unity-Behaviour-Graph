using System;
using System.Reflection;
using Sirenix.OdinInspector;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    public static class AttributeCache <Attr>
        where Attr : Attribute
    {
        private static ValueDropdownList<string> cachedValueDropdown;
        public static ValueDropdownList<string> GetCachedMemberDropdown()
        {
            if (cachedValueDropdown != null)
                return cachedValueDropdown;
            
            cachedValueDropdown = new ValueDropdownList<string>();
            if (BehaviourGraphSMS.TryGetMembersByAttribute<Attr>(out var members))
            {
                foreach (var member in members)
                {
                    string memberReflectName = BehaviourGraphSMS.MemberToString(member);
                    cachedValueDropdown.Add(memberReflectName, memberReflectName);
                }
            }
            return cachedValueDropdown;
        }

        public static bool TryGetCachedMemberViaLookupValue(string value, out MemberInfo info)
        {
            info = BehaviourGraphSMS.LookupByString(value);
            if (info == null)
                return false;
            return true;
        }
    }
}
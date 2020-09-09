using System;
using System.Reflection;
using BehaviourGraph.CodeLinks.AttributeCache;
using BehaviourGraph.Conditionals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [Serializable]
    internal class ConditionalSelector
    {
        [HideInInspector]
        public bool isMethod = false;
        
        [ValueDropdown("GetMembers", NumberOfItemsBeforeEnablingSearch = 2)]
        [SerializeField]
        private string memberSelector = "";

        private MemberInfo selectedMember = null;
        private string previousSelection;
        public MemberInfo MemberSelector
        {
            get
            {
                if (selectedMember != null && previousSelection == memberSelector)
                {
                    return selectedMember;
                }
                
                if (AttributeCache<Condition>.
                    TryGetCachedMemberViaLookupValue(memberSelector, out var temp))
                {
                    isMethod = (temp.MemberType & MemberTypes.Method) != 0;

                    selectedMember = temp;
                    previousSelection = memberSelector;
                    return selectedMember;
                }

                return null;
            }
        }

        public ValueDropdownList<string> GetMembers => 
            AttributeCache<Condition>.GetCachedMemberDropdown();
    }
}
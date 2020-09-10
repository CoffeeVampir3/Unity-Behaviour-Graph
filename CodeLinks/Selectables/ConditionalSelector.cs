using System;
using System.Reflection;
using BehaviourGraph.Attributes;
using BehaviourGraph.CodeLinks.AttributeCache;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    [Serializable]
    public class ConditionalSelector
    {
        [ValueDropdown("GetMembers", NumberOfItemsBeforeEnablingSearch = 2)]
        [SerializeField]
        private string memberSelector = "";
        public ValueDropdownList<string> GetMembers => 
            AttributeCache<Condition>.GetCachedMemberDropdown();
        
        [HideInInspector]
        public bool isMethod = false;

        #region Accessors
        
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
        
        #endregion
        
    }
}
using System;
using BehaviourGraph.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    [Serializable]
    public class ServiceSelector
    {
        [SerializeField]
        [ValueDropdown("GetServices", NumberOfItemsBeforeEnablingSearch = 2)]
        public string targetMethod = null;
        public ValueDropdownList<string> GetServices => 
            AttributeCache<Service>.GetCachedMemberDropdown();
    }
}
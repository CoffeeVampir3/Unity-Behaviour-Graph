using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using BehaviourGraph.CodeLinks.AttributeCache;
using BehaviourGraph.Conditionals;
using BehaviourGraph.Services;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Expression = System.Linq.Expressions.Expression;

namespace BehaviourGraph.Debugging
{
    public class CacheDebugger : SerializedMonoBehaviour
    {
        [NonSerialized, OdinSerialize] 
        private BehaviourGraphSMS store;
        
        [SerializeField]
        [ValueDropdown("GetServices", NumberOfItemsBeforeEnablingSearch = 2)]
        public string targetMethod = null;
        public ValueDropdownList<string> GetServices => AttributeCache<Service>.GetCachedMemberDropdown();

        [Button]
        public void DebugService()
        {
            if (AttributeCache<Service>.TryGetCachedMemberViaLookupValue(targetMethod, 
                out var method))
            {
                Debug.Log("yote");
                RuntimeService rtService = new RuntimeService();
                rtService.Initialize(method as MethodInfo, gameObject);
                StartCoroutine(rtService.executable(gameObject));
            }
        }

        [Service]
        public IEnumerator TestService(GameObject go)
        {
            Debug.Log("???");
            yield return null;
        }

        [SerializeField, ValueDropdown("GetConditionMembers")] 
        public string conditionSelector;
        public ValueDropdownList<string> GetConditionMembers => 
            AttributeCache<Condition>.GetCachedMemberDropdown();
        
        [SerializeField, ValueDropdown("GetServiceMembers")] 
        public string serviceSelector;
        public ValueDropdownList<string> GetServiceMembers => 
            AttributeCache<Service>.GetCachedMemberDropdown();
        
        
        private Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
        
    }
}
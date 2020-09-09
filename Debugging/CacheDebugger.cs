using System;
using System.Linq;
using System.Reflection;
using BehaviourGraph.CodeLinks;
using BehaviourGraph.CodeLinks.AttributeCache;
using BehaviourGraph.Conditionals;
using BehaviourGraph.Services;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using Expression = System.Linq.Expressions.Expression;

namespace BehaviourGraph.Debugging
{
    public class CacheDebugger : SerializedMonoBehaviour
    {

        [NonSerialized, OdinSerialize] 
        private BehaviourGraphSMS store;

        [Button]
        public void Path()
        {
            MonoScript thisScript = MonoScript.FromMonoBehaviour(this);
            Debug.Log(AssetDatabase.GetAssetPath(thisScript));
        }

        [SerializeField, ValueDropdown("GetConditionMembers")] 
        public string conditionSelector;
        public ValueDropdownList<string> GetConditionMembers => 
            AtribCache<Condition>.GetCachedMemberDropdown();
        
        [SerializeField, ValueDropdown("GetServiceMembers")] 
        public string serviceSelector;
        public ValueDropdownList<string> GetServiceMembers => 
            AtribCache<Service>.GetCachedMemberDropdown();
        
        
        private Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
        
    }
}
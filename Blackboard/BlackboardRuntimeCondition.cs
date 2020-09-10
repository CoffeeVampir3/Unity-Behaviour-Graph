using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [Serializable]
    public class BlackboardRuntimeCondition
    {
        private FieldInfo field;
        private Component cachedConditionComponent;
        private Func<bool> evalAction;

        private bool EvaluateField()
        {
            return (bool)field.GetValue(cachedConditionComponent);
        }
        private Func<bool> InvalidStub = () => false;

        public bool Evaluate()
        {
            return evalAction.Invoke();
        }

        public BlackboardRuntimeCondition(ConditionalSelector selector, GameObject target)
        {
            var member = selector.MemberSelector;
            Type declType = member.DeclaringType;
            
            if (!target.TryGetComponent(member.DeclaringType, out cachedConditionComponent))
            {
                Debug.LogError(
                    "Could not create condition for: " + 
                    target.name + " using " + declType?.Name);
                
                evalAction = InvalidStub;
                return;
            }
            switch (selector.isMethod)
            {
                case true:
                    BuildConditionFunction(target, member as MethodInfo, declType);
                    break;
                default:
                    field = member as FieldInfo;
                    evalAction = EvaluateField;
                    break;
            }
        }

        private void BuildConditionFunction(GameObject obj, MethodInfo method, Type condType)
        {
            if (!obj.TryGetComponent(condType, out cachedConditionComponent))
            {
                Debug.LogError(
                    "Unable to find condition " + condType.Name + " in " + obj.name);
                
                evalAction = InvalidStub;
            }
            evalAction = CreateConditionFunction(method, cachedConditionComponent);
        }
        
        private Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType;
            
            getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(
                getType(types.ToArray()), target, methodInfo.Name);
        }
    }
}
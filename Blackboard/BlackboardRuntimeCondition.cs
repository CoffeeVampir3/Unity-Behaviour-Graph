using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BehaviourGraph.Conditionals;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [Serializable]
    public class BlackboardRuntimeCondition
    {
        private bool isMethod = false;
        private Type conditionType;
        private MethodInfo method;
        private FieldInfo field;
        private Func<bool> cachedConditionFunction;

        private int cachedObjectHash = -1;
        private Component cachedConditionComponent;
        public bool EvaluateAs(GameObject obj)
        {
            if (obj == null)
                return false;
            
            if (cachedObjectHash != obj.GetHashCode())
            {
                cachedObjectHash = obj.GetHashCode();
                SetupCachedVersionFor(obj);
            }
            
            if (isMethod)
            {
                return cachedConditionFunction.Invoke();
            }

            return (bool)field.GetValue(cachedConditionComponent);
        }

        public void SetupCachedVersionFor(GameObject obj)
        {
            if (!obj.TryGetComponent(conditionType, out cachedConditionComponent))
            {
                throw new Exception("Unable to find condition " + conditionType.Name + " in " + obj.name);
            }
            if (isMethod)
            {
                cachedConditionFunction = CreateConditionFunction(method, cachedConditionComponent);
            }
        }
        
        public Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType;
            
            getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
        
        public void RuntimeCacheSetup(ConditionalSelector selector)
        {
            isMethod = selector.isMethod;
            conditionType = selector.conditionClassSelector;
            
            if (isMethod)
            {
                method = selector.methodSelector;
            }
            else
            {
                int fieldIndex = selector.fieldSelector;

                FieldInfo[] fields;
                if (ConditionalCache.TryGetCondition(conditionType, out fields))
                {
                    field = fields[fieldIndex];
                }
                else
                {
                    throw new Exception("Unable to create field info for type: " + conditionType.Name +
                                        " with index: " + fieldIndex);
                }
            }
        }
    }
}
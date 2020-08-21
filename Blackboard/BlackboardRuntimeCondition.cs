using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BehaviourGraph.Conditionals;

namespace BehaviourGraph.Blackboard
{
    [Serializable]
    public class BlackboardRuntimeCondition
    {
        private bool isMethod = false;
        private MethodInfo method;
        private FieldInfo field;
        private Delegate cachedMethodDelegate;

        private int cachedObjectHash = -1;
        public bool EvaluateAs(object o)
        {
            if (o == null)
                return false;
            
            if (isMethod)
            {
                if (cachedObjectHash != o.GetHashCode())
                {
                    cachedObjectHash = o.GetHashCode();
                    SetupCachedVersionFor(o);
                }

                return (bool)cachedMethodDelegate.DynamicInvoke();
            }

            return (bool)field.GetValue(o);
        }

        public void SetupCachedVersionFor(object o)
        {
            if (isMethod)
            {
                cachedMethodDelegate = CreateDelegate(method, o);
            }
        }
        
        public Delegate CreateDelegate(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType;
            var isAction = methodInfo.ReturnType == typeof(void);
            var types = methodInfo.GetParameters().Select(p => p.ParameterType);

            if (isAction) {
                getType = Expression.GetActionType;
            }
            else {
                getType = Expression.GetFuncType;
                types = types.Concat(new[] { methodInfo.ReturnType });
            }

            if (methodInfo.IsStatic) {
                return Delegate.CreateDelegate(getType(types.ToArray()), methodInfo);
            }

            return Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
        
        public void RuntimeCacheSetup(ConditionalSelector selector, Type selectedConditionType)
        {
            isMethod = selector.isMethod;

            if (isMethod)
            {
                method = selector.methodSelector;
            }
            else
            {
                int fieldIndex = selector.fieldSelector;

                FieldInfo[] fields;
                if (ConditionalCache.TryGetCondition(selectedConditionType, out fields))
                {
                    field = fields[fieldIndex];
                }
                else
                {
                    throw new Exception("Unable to create field info for type: " + selectedConditionType.Name +
                                        " with index: " + fieldIndex);
                }
            }
        }
    }
}
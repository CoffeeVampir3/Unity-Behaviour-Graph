using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BehaviourGraph.Services
{
    internal static class ServiceCreator
    {
        static readonly Type[] serviceFunctionPattern = { typeof(IEnumerator)};
        public static Func<IEnumerator> CreateServiceFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType = Expression.GetFuncType;
            var types = serviceFunctionPattern;

            return (Func<IEnumerator>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
    }
}
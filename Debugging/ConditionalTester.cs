using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourGraph.Conditionals
{
    [ExecuteAlways]
    public class ConditionalTester : SerializedMonoBehaviour
    {
        public GameObject testObj;
        private Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }

        [Condition] 
        public bool proceedIfTrue = false;

        [Condition]
        public bool mooses02 = true;
        [Condition]
        public bool mooses1 = true;
        [Condition]
        public bool mooses2 = true;
        [Condition]
        public bool mooses3 = true;
        [Condition]
        public bool mooses4 = true;

        [Condition]
        public bool DoThing()
        {
            return true;
        }
        
        [Condition]
        public bool OtherThing()
        {
            return false;
        }
    }
}
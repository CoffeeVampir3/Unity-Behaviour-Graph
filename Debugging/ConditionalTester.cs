using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace BehaviourGraph.Conditionals
{
    [ExecuteAlways]
    public class ConditionalTester : MonoBehaviour
    {
        [Button]
        public void TestCachedConditions()
        {
            MethodInfo[] methods;
            FieldInfo[] fields;

            if (ConditionalCache.TryGetCondition(GetType(), out fields))
            {
                foreach (var q in fields)
                {
                    Debug.Log(q.GetNiceName());
                    Debug.Log(q.GetValue(this));
                }
            }
            
            if (ConditionalCache.TryGetCondition(GetType(), out methods))
            {
                foreach (var q in methods)
                {
                    Debug.Log(q.GetNiceName());
                }
            }
        }

        [Condition] 
        public bool proceedIfTrue = false;

        [Condition]
        public bool mooses0 = true;
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
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    public interface IBlackboardReference
    {
        GameObject GetReference();
        void SetReference(GameObject go);
        
        void CacheRuntimeValues();
        bool Evaluate();
    }
}
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [CreateAssetMenu]
    public class Blackboard : SerializedScriptableObject, IBlackboard
    {
        [SerializeField] 
        private int blackboardInitialDataAllocationSize = 100;
        
        [NonSerialized, OdinSerialize]
        private BlackboardDefaultDataContainer blackboard = new BlackboardDefaultDataContainer();

        public void Initialize(int allocationSize)
        {
            blackboard.Initialize(allocationSize);
        }
        
        #if UNITY_EDITOR
        private void AllocateNewBlackboard()
        {
            if (blackboardInitialDataAllocationSize <= 0)
            {
                Debug.LogError("Attempted to allocate a negative blackboard array size.");
                return;
            }
            blackboard.Initialize(blackboardInitialDataAllocationSize);
        }
        
        public void Reset()
        {
            AllocateNewBlackboard();
        }

        public void OnValidate()
        {
            AllocateNewBlackboard();
        }
        #endif

        public T GetItem<T>(int index)
        {
            return blackboard.GetItem<T>(index);
        }

        public object GetItem(int index)
        {
            return blackboard.GetItem(index);
        }

        public Type GetType(int index)
        {
            return blackboard.GetType(index);
        }

        public int SetItem<T>(T item)
        {
            return blackboard.SetItem(item);
        }

        public void ReleaseId(int id)
        {
            blackboard.ReleaseId(id);
        }
    }
}
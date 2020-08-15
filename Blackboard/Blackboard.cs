using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    public class Blackboard : ScriptableObject, IBlackboard
    {
        [SerializeField]
        private BlackboardBase blackboard = new BlackboardBase();
        
        public T GetItem<T>(int index)
        {
            return blackboard.GetItem<T>(index);
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
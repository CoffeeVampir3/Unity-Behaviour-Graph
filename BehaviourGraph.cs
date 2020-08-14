using UnityEngine;
using XNode;

namespace Coffee.BehaviourGraph
{
    public class BehaviourGraph : NodeGraph
    {
        [SerializeField]
        private GameObject pawn;

        public void Init(GameObject owner)
        {
            pawn = owner;
        }

        public void CompileGraph()
        {
        }
    }

}

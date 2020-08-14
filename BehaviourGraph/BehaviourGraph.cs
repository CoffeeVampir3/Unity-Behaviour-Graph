using UnityEngine;
using XNode;

namespace Coffee.Behaviour
{
    [CreateAssetMenu]
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

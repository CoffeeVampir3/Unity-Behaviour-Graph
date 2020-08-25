using BehaviourGraph.Debugging;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Coffee.Behaviour.Debugging
{
    public class BehaviourGraphDebugger : MonoBehaviour
    {
        [SerializeField] 
        private BehaviourGraph graph = null;

        [SerializeField]
        private Salami injectionTest;

        [Button]
        private void RunGraph()
        {
            if (graph.tree != null)
            {
                graph.tree.RuntimeSetup(this.gameObject);
                graph.tree.Tick();
            } 
        }
    }
}
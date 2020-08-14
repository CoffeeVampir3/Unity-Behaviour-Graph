using UnityEngine;
using Sirenix.OdinInspector;

namespace Coffee.Behaviour.Debugging
{
    public class BehaviourGraphDebugger : MonoBehaviour
    {
        [SerializeField] 
        private BehaviourGraph graph = null;

        [Button]
        private void RunGraph()
        {
            if (graph.tree != null)
            {
                Debug.Log("Running graphs!");
                graph.tree.Tick();
            }
        }
    }
}
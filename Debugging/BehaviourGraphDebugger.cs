using UnityEngine;
using Sirenix.OdinInspector;

namespace Coffee.Behaviour.Debugging
{
    public class BehaviourGraphDebugger : MonoBehaviour
    {
        [SerializeField] 
        private BehaviourGraph tg = null;

        [Button]
        private void RunGraph()
        {
            tg.Execute(gameObject);
        }
    }
}
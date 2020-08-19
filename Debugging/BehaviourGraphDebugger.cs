using System.Reflection;
using BehaviourGraph.Conditionals;
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

        [SerializeField]
        private int lastIndex;
        [Button]
        private void InjectBlackboardTestValues()
        {
            lastIndex = graph.blackboard.SetItem(injectionTest);
            Debug.Log(lastIndex);
        }

        [Button]
        private void DebugInjectionValues()
        {
            ConditionalCache.InitializeCache();
            FieldInfo[] infos;
            ConditionalCache.TryGetCondition(typeof(Salami), out infos);
            for (int i = 0; i <= lastIndex; i++)
            {
                Salami sal = graph.blackboard.GetItem<Salami>(i);
                Debug.Log(sal);
                foreach (FieldInfo f in infos)
                {
                    bool b = (bool) f.GetValue(sal);
                    f.GetValue(sal);
                    Debug.Log(b);
                }
            }
        }
        
        [Button]
        private void RunGraph()
        {
            if (graph.tree != null)
            {
                graph.tree.Tick();
            } 
        }
    }
}
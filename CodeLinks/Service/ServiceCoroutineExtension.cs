using System.Collections;
using UnityEngine;

namespace BehaviourGraph.Services
{
    
    //Thanks guys on stackoverflow <3
    internal class ServiceCoroutineExtension
    {
        public enum CoroutineState
        {
            Ready,
            Running,
            Finished
        }
 
        public class CoroutineController
        {
            private Coroutine coroutine;
            public CoroutineState state;
 
            public CoroutineController()
            {
                state = CoroutineState.Ready;
            }
 
            public void Start(IEnumerator routine)
            {
                state = CoroutineState.Running;
                coroutine = CoroutineHelper.Instance.StartCoroutine(RealRun(routine));
            }
 
            private IEnumerator RealRun(IEnumerator routine)
            {
                yield return CoroutineHelper.Instance.StartCoroutine(routine);
                state = CoroutineState.Finished;
            }

            public void Finish()
            {
                CoroutineHelper.Instance.StopCoroutine(coroutine);
                coroutine = null;
                state = CoroutineState.Ready;
            }
        }
        
        public class CoroutineHelper : MonoBehaviour
        {
            private static CoroutineHelper ins;
            public static CoroutineHelper Instance
            {
                get
                {
                    if (ins == null)
                    {
                        var go = new GameObject("CoroutineHelper");
                        ins = go.AddComponent<CoroutineHelper>();
                    }
                    return ins;
                }
            }
        }
    }
}
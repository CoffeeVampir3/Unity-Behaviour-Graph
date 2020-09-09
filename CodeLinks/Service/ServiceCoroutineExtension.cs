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
            private IEnumerator routine;
            private Coroutine coroutine;
            private Coroutine helperRoutine;
            public CoroutineState state;
 
            public CoroutineController(IEnumerator routine)
            {
                this.routine = routine;
                state = CoroutineState.Ready;
            }
 
            public void Start()
            {
                state = CoroutineState.Running;
                coroutine = CoroutineHelper.Instance.StartCoroutine(RealRun());
            }
 
            private IEnumerator RealRun()
            {
                helperRoutine = CoroutineHelper.Instance.StartCoroutine(routine);
                yield return helperRoutine;
                state = CoroutineState.Finished;
                CoroutineHelper.Instance.StopCoroutine(coroutine);
            }

            public void Finish()
            {
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
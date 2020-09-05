using System.Collections;
using UnityEngine;

namespace Coffee.BehaviourTree.Leaf
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
                yield return CoroutineHelper.Instance.StartCoroutine(routine);
                state = CoroutineState.Finished;
            }
 
            public void Stop()
            {
                CoroutineHelper.Instance.StopCoroutine(coroutine);
                state = CoroutineState.Finished;
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
                        DontDestroyOnLoad(go);
                        ins = go.AddComponent<CoroutineHelper>();
                    }
                    return ins;
                }
            }
 
            public void StartCoroutineEx(IEnumerator routine, out CoroutineController coroutineController)
            {
                coroutineController = new CoroutineController(routine);
                coroutineController.Start();
            }
        }
    }
}
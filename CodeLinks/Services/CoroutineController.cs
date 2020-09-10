using System.Collections;
using UnityEngine;

namespace BehaviourGraph.Services
{
    //Thanks guys on stackoverflow <3
    //This lets us track when a coroutine completes.
    internal class CoroutineController
    {
        private Coroutine coroutine;
        private MonoBehaviour coroutineContext;
        public CoroutineState state;
        
        public enum CoroutineState
        {
            Ready,
            Running,
            Finished
        }

        public CoroutineController()
        {
            state = CoroutineState.Ready;
        }

        public void Start(MonoBehaviour ctx, IEnumerator routine)
        {
            coroutineContext = ctx;
            state = CoroutineState.Running;
            coroutine = coroutineContext.StartCoroutine(RealRun(routine));
        }

        private IEnumerator RealRun(IEnumerator routine)
        {
            yield return coroutineContext.StartCoroutine(routine);
            state = CoroutineState.Finished;
        }

        public void Finish()
        {
            coroutineContext.StopCoroutine(coroutine);
            coroutine = null;
            state = CoroutineState.Ready;
        }
    }
}
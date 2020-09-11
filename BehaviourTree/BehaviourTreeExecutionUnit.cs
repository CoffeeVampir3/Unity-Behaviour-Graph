using UnityEngine;

namespace Coffee.BehaviourTree
{
    public partial class BehaviourTree
    {
        private void ExecuteTree()
        {
            var currentNode = contextWalker.GetContextNode();
            
            var ctxResult = currentNode.Execute();
            if (ctxResult == TreeBaseNode.Result.Running)
                return;
        }
    }
}
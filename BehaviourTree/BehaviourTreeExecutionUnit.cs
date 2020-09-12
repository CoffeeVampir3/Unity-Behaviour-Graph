using System;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public partial class BehaviourTree
    {
        private void ExecuteTree()
        {
            while (true)
            {
                var currentNode = contextWalker.GetContextNode();

                var ctxResult = currentNode.Execute();
                switch (ctxResult)
                {
                    case TreeBaseNode.Result.Failure:
                        if (!contextWalker.Unwind()) 
                            return;
                        contextWalker.Reset();
                        continue;
                    
                    case TreeBaseNode.Result.Success:
                        if (!contextWalker.Unwind()) 
                            return;
                        contextWalker.Reset();
                        continue;
                }
                break;
            }
        }
    }
}
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
                if (ctxResult != TreeBaseNode.Result.Running)
                {
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
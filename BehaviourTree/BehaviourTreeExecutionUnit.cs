namespace Coffee.BehaviourTree
{
    public partial class BehaviourTree
    {
        /// <summary>
        /// Context walker starts its journey at the root node upon initialization.
        /// The first execution therefore always runs starting from the root (duh)
        /// When a condition node is executed, the context drops into that condition's sub-context.
        /// Whenever a condition either completes or fails, the context walker will unwind to
        /// the parent context and try evaluating again until it climbs all the way back up to the
        /// root context.
        /// </summary>
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
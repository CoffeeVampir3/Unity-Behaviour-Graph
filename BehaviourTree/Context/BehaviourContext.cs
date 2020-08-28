namespace Coffee.BehaviourTree.Context
{
    internal class BehaviourContext
    {
        internal readonly TreeBaseNode node;
        internal readonly TreeBaseNode.Result result;

        internal BehaviourContext()
        {
            node = null;
            result = TreeBaseNode.Result.Running;
        }
        
        internal BehaviourContext(TreeBaseNode node, TreeBaseNode.Result result)
        {
            this.node = node;
            this.result = result;
        }
    }
}
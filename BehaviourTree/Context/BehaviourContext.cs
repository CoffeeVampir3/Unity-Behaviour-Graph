namespace Coffee.BehaviourTree.Context
{
    internal class BehaviourContext
    {
        internal TreeBaseNode node;
        internal TreeBaseNode.Result result;

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

        internal void SetContext(TreeBaseNode node, TreeBaseNode.Result result)
        {
            this.node = node;
            this.result = result;
        }

        internal void Reset()
        {
            node = null;
            result = TreeBaseNode.Result.Running;
        }
    }
}
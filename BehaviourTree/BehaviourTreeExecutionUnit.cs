namespace Coffee.BehaviourTree
{
    public partial class BehaviourTree
    {
        private void ExecuteTree()
        {
            while (context.node != null)
            {
                context.node.Execute(ref context);
                if (context == null || context.result == TreeBaseNode.Result.Waiting)
                    break;
            }

            UnityEngine.Debug.Assert(context != null, nameof(context) + " != null");
            if (context.node == null)
            {
                root.Execute(ref context);
            }
        }
    }
}
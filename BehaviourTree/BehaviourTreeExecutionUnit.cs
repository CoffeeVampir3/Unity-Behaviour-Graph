using UnityEngine;

namespace Coffee.BehaviourTree
{
    public partial class BehaviourTree
    {
        private void ExecuteTree()
        {
            UnityEngine.Debug.Assert(context != null, nameof(context) + " != null");
            while (context.node != null)
            {
                Debug.Log("Had context: " + context.node.GetType());
                context.node.Execute(ref context);
                if (context.result == TreeBaseNode.Result.Running)
                    return;
            }
            
            UnityEngine.Debug.Assert(context != null, nameof(context) + " != null");
            if (context.node == null)
            {
                root.Execute(ref context);
            }
        }
    }
}
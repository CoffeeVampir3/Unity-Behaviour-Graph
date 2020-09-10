using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public class BehaviourTree
    {
        internal GameObject owner;
        private ITreeBehaviourNode root;
        private BehaviourContext context;

        public BehaviourTree(GameObject owner)
        {
            this.owner = owner;
        }

        internal void RuntimeSetup(TreeBaseNode newRoot, GameObject owningObject)
        {
            root = newRoot;
            owner = owningObject;
            context = new BehaviourContext();
        }

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
        
        public void Tick()
        {
            ExecuteTree();
        }
    }
}
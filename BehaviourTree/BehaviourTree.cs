using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    /// <summary>
    /// Compiled representation of our behaviour graph.
    /// <para>Run the tree using Evaluate()</para>
    /// </summary>
    public partial class BehaviourTree
    {
        internal GameObject owner;
        private TreeBaseNode root;
        internal ContextWalker contextWalker;

        public BehaviourTree(GameObject owner)
        {
            this.owner = owner;
        }

        internal void RuntimeSetup(TreeBaseNode newRoot, GameObject owningObject)
        {
            root = newRoot;
            owner = owningObject;
            contextWalker = new ContextWalker();
            contextWalker.SetContextPointer(newRoot.context);
        }

        public void Evaluate()
        {
            ExecuteTree();
        }
    }
}
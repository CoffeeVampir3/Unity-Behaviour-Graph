using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public partial class BehaviourTree
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

        public void Tick()
        {
            ExecuteTree();
        }
    }
}
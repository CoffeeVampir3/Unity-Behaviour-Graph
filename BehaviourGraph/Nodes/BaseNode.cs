using Coffee.BehaviourTree;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour.Nodes
{
    public abstract class BaseNode : Node
    {
        [SerializeField]
        [HideInInspector]
        public TreeBaseNode thisTreeNode;
        [SerializeField]
        [HideInInspector]
        protected BehaviourGraph parentGraph;
        [SerializeField]
        [HideInInspector]
        protected BehaviourTree.BehaviourTree parentTree;

        protected abstract void OnCreation();

        [SerializeField]
        [HideInInspector]
        private bool initialized = false;
        protected override void Init()
        {
            if (initialized)
                return;
            
            base.Init();
            parentGraph = graph as BehaviourGraph;
            parentTree = parentGraph.tree;
            initialized = true;

            OnCreation();
        }
        
        public override object GetValue(NodePort port)
        {
            return thisTreeNode;
        }
    }
}
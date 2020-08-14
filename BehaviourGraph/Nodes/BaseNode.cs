using Coffee.BehaviourTree;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour.Nodes
{
    public abstract class BaseNode : Node
    {
        [HideInInspector]
        public TreeBaseNode thisTreeNode;
        [SerializeField]
        protected BehaviourGraph parentGraph;
        [SerializeField]
        protected BehaviourTree.BehaviourTree parentTree;

        [SerializeField] 
        private bool initialized = false;
        protected override void Init()
        {
            if (initialized)
                return;
            
            base.Init();
            parentGraph = graph as BehaviourGraph;
            parentTree = parentGraph.tree;
            initialized = true;
        }
        
        public override object GetValue(NodePort port)
        {
            return thisTreeNode;
        }
    }
}
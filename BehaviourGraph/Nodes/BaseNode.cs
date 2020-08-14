using Coffee.BehaviourTree;
using UnityEngine;
using XNode;

namespace Coffee.Behaviour.Nodes
{
    public abstract class BaseNode : Node
    {
        [HideInInspector]
        public ITreeBehaviourNode thisNode;
        protected BehaviourGraph parentGraph;
        protected BehaviourTree.BehaviourTree parentTree;
        
        protected override void Init()
        {
            base.Init();
            parentGraph = graph as BehaviourGraph;
            parentTree = parentGraph.tree;
        }
        
        public override object GetValue(NodePort port)
        {
            return thisNode;
        }
    }
}
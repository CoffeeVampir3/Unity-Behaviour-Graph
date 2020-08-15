using System;
using UnityEngine;
using XNode;
using Debug = System.Diagnostics.Debug;

namespace Coffee.Behaviour.Nodes.BlackboardNodes
{
    [Serializable]
    public abstract class BlackboardBaseNode : Node
    {
        [SerializeField]
        [HideInInspector]
        protected BehaviourGraph parentGraph;
        [SerializeField]
        [HideInInspector]
        protected BehaviourTree.BehaviourTree parentTree;

        [SerializeField]
        [HideInInspector]
        private bool initialized = false;
        protected override void Init()
        {
            if (initialized)
                return;
            
            base.Init();
            parentGraph = graph as BehaviourGraph;
            Debug.Assert(parentGraph != null, nameof(parentGraph) + " != null");
            
            parentTree = parentGraph.tree;
            initialized = true;
        }
    }
}
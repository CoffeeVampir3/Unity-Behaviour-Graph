using System.Collections.Generic;
using BehaviourGraph.Blackboard;
using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public class BehaviourTree
    {
        internal GameObject owner;
        private List<Blackboard> blackboards;
        private ITreeBehaviourNode root;
        private BehaviourContext context;

        internal void Init(ITreeBehaviourNode rootNode, ref List<Blackboard> sharedBb)
        {
            root = rootNode;
            blackboards = sharedBb;
        }

        internal void RuntimeSetup(GameObject owner)
        {
            this.owner = owner;
            context = new BehaviourContext();
            for (int i = 0; i < blackboards.Count; i++)
            {
                blackboards[i].RuntimeInitialize(owner);
            }
        }

        private void ExecuteTree()
        {
            while (context.node != null)
            {
                context.node.Execute(ref context);
                if (context == null || context.result == TreeBaseNode.Result.Waiting)
                    break;
            }
            
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
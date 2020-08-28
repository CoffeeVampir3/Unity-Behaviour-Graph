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

        public void ExecuteTestTree()
        {
            int breaksafe = 0;
            if(root == null)
                Debug.Log("Null root.");
            
            root.Reset();
            Debug.Log("Executing 100 cycle test.");
            while (root.Execute(ref context) == TreeBaseNode.Result.Running)
            {
                breaksafe++;
                if (breaksafe > 99)
                {
                    break;
                }
            }
            
            Debug.Log("Done.");
        }

        private void ExecuteTree()
        {
            if (context?.node == null)
            {
                root.Reset();
                root.Execute(ref context);
            }
            
            while (context?.node != null)
            {
                if (context.result == TreeBaseNode.Result.Waiting)
                    return;
                context.node.Execute(ref context);
            }
        }
        
        public void Tick()
        {
            ExecuteTree();
        }
    }
}
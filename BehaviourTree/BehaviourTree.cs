using System.Collections.Generic;
using BehaviourGraph.Blackboard;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public class BehaviourTree
    {
        private GameObject owner;
        private bool startedBehaviour;
        private LocalBlackboard localBlackboard;
        private List<SharedBlackboard> blackboards;
        private ITreeBehaviourNode root;

        internal void Init(ITreeBehaviourNode rootNode, ref LocalBlackboard localBb, ref List<SharedBlackboard> sharedBb)
        {
            startedBehaviour = false;
            root = rootNode;
            localBlackboard = localBb;
            blackboards = sharedBb;
        }

        internal void RuntimeSetup(GameObject owner)
        {
            this.owner = owner;
            localBlackboard.RuntimeInitialize(owner);
        }

        public void ExecuteTestTree()
        {
            int breaksafe = 0;
            if(root == null)
                Debug.Log("Null root.");
            
            root.Reset();
            Debug.Log("Executing 100 cycle test.");
            while (root.Execute() == TreeBaseNode.Result.Running)
            {
                breaksafe++;
                if (breaksafe > 99)
                {
                    break;
                }
            }
            
            Debug.Log("Done.");

            startedBehaviour = false;
        }

        private void ExecuteTree()
        {
            while (root.Execute() == TreeBaseNode.Result.Running)
            {
            }
        }

        public void Tick()
        {
            if (startedBehaviour) 
                return;
            
            startedBehaviour = true;
            ExecuteTree();
        }
    }
}
using System.Collections.Generic;
using BehaviourGraph.Blackboard;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public class BehaviourTree
    {
        internal GameObject owner;
        private bool startedBehaviour;
        private List<Blackboard> blackboards;
        private ITreeBehaviourNode root;

        internal void Init(ITreeBehaviourNode rootNode, ref List<Blackboard> sharedBb)
        {
            startedBehaviour = false;
            root = rootNode;
            blackboards = sharedBb;
        }

        internal void RuntimeSetup(GameObject owner)
        {
            this.owner = owner;
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
            var result = root.Execute();
        }
        
        public void Tick()
        {
            ExecuteTree();
        }
    }
}
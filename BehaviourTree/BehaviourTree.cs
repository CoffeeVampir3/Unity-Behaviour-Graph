using System.Collections.Generic;
using Coffee.BehaviourTree.Composite;
using Coffee.BehaviourTree.Decorator;
using Coffee.BehaviourTree.Leaf;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public class BehaviourTree
    {
        private bool startedBehaviour;
        public Dictionary<string, object> Blackboard { get; set; }

        private ITreeBehaviourNode root;
        public ITreeBehaviourNode Root => root;
        
        public void Awake()
        {
            Blackboard = new Dictionary<string, object>();

            startedBehaviour = false;
        }

        private void ExecuteTestTree()
        {
            int breaksafe = 0;
            while (root.Execute() == TreeBaseNode.Result.Running)
            {
                breaksafe++;
                if (breaksafe > 100)
                {
                    break;
                }
            }

            startedBehaviour = false;
        }

        private void ExecuteTree()
        {
            ExecuteTestTree();
        }

        public void Tick()
        {
            if (!startedBehaviour)
            {
                startedBehaviour = true;
                ExecuteTree();
            }
        }
    }
}
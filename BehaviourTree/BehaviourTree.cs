using System.Collections.Generic;
using Coffee.BehaviourTree.Composite;
using Coffee.BehaviourTree.Decorator;
using Coffee.BehaviourTree.Leaf;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public class BehaviourTree : MonoBehaviour
    {
        private bool startedBehaviour;
        public Dictionary<string, object> Blackboard { get; set; }

        private IBehaviourNode root;
        public IBehaviourNode Root => root;
        
        public void Awake()
        {
            Blackboard = new Dictionary<string, object>();

            startedBehaviour = false;
        }

        private void ExecuteTestTree()
        {
            root = new RepeaterNode(this,
                new SequencerNode(this,
                    new IBehaviourNode[] { new LeafTesterNode(this) }));
            
            int breaksafe = 0;
            while (root.Execute() == BaseNode.Result.Running)
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
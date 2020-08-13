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
        
        public enum Result
        {
            Failure,
            Success,
            Running
        }

        public void Awake()
        {
            Blackboard = new Dictionary<string, object>();

            startedBehaviour = false;
            
            root = new RepeaterNode(this,
                new SequencerNode(this,
                    new IBehaviourNode[] { new LeafTesterNode(this) }));
        }

        private void ExecuteTree()
        {
            int breaksafe = 0;
            while (root.Execute() == Result.Running)
            {
                breaksafe++;
                if (breaksafe > 100)
                {
                    Debug.Log("Infinite");
                    break;
                }

                
            }

            startedBehaviour = false;
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
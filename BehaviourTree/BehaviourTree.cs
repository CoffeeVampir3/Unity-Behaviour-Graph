using System;
using System.Collections.Generic;
using BehaviourGraph.Blackboard;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public class BehaviourTree : SerializedScriptableObject
    {
        [SerializeField]
        private bool startedBehaviour;
        [SerializeField]
        public IBlackboard blackboard;
        [SerializeField]
        [HideInInspector]
        public TreeBaseNode root;

        public void Init()
        {
            startedBehaviour = false;
        }

        private void ExecuteTestTree()
        {
            int breaksafe = 0;
            if(root == null)
                Debug.Log("Null root.");
            
            root.Reset();
            Debug.Log("Executing 100 cycle test.");
            while (root.Execute() == TreeBaseNode.Result.Running)
            {
                breaksafe++;
                if (breaksafe > 100)
                {
                    break;
                }
            }
            
            Debug.Log("Done.");

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
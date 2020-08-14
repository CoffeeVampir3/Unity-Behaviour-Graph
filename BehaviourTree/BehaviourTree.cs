using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    [Serializable]
    public class BehaviourTree : SerializedScriptableObject
    {
        [SerializeField]
        private bool startedBehaviour;
        public Dictionary<string, object> Blackboard { get; set; }
        [SerializeField]
        public List<TreeBaseNode> nodes;
        [SerializeField]
        public TreeBaseNode root;

        public void Init()
        {
            Debug.Log("Initialized tree.");
            Blackboard = new Dictionary<string, object>();
            nodes = new List<TreeBaseNode>();
            startedBehaviour = false;
        }

        private void ExecuteTestTree()
        {
            int breaksafe = 0;
            if(root == null)
                Debug.Log("Null root.");
            
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
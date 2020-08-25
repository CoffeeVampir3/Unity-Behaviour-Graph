using System;
using System.Collections.Generic;
using BehaviourGraph.Blackboard;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public class BehaviourTree : SerializedScriptableObject
    {
        [NonSerialized] 
        protected GameObject owner;
        [SerializeField, HideInInspector]
        protected bool startedBehaviour;
        [SerializeField]
        public LocalBlackboard localBlackboard;
        [SerializeField] 
        public List<SharedBlackboard> blackboards;
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        protected ITreeBehaviourNode root;

        public void Init(ITreeBehaviourNode rootNode, ref LocalBlackboard localBb, ref List<SharedBlackboard> sharedBb)
        {
            startedBehaviour = false;
            root = rootNode;
            localBlackboard = localBb;
            blackboards = sharedBb;
        }

        public void RuntimeSetup(GameObject owner)
        {
            this.owner = owner;
            localBlackboard.RuntimeInitialize(owner);
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
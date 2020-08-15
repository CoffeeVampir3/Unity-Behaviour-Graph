﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    public class BehaviourTree : SerializedScriptableObject
    {
        private bool startedBehaviour;
        //TODO:: Actual blackboard implementation.
        public Dictionary<string, object> Blackboard { get; set; }
        [SerializeField]
        [HideInInspector]
        public TreeBaseNode root;

        public void Init()
        {
            Blackboard = new Dictionary<string, object>();
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
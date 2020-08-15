using System;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    [Serializable]
    public abstract class TreeBaseNode : ITreeBehaviourNode
    {
        [SerializeField]
        [HideInInspector]
        protected BehaviourTree parentTree;
        
        public enum Result
        {
            Failure,
            Success,
            Running
        }
        
        protected TreeBaseNode(BehaviourTree tree)
        {
            parentTree = tree;
        }

        public abstract Result Execute();

        public abstract void Reset();
    }
}
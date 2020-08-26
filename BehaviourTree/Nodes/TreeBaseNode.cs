using System;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    [Serializable]
    internal abstract class TreeBaseNode : ITreeBehaviourNode
    {
        [NonSerialized, OdinSerialize]
        [HideInInspector]
        public BehaviourTree parentTree;
        
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
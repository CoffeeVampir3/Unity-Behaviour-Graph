using UnityEngine;

namespace Coffee.BehaviourTree
{
    internal abstract class TreeBaseNode : ITreeBehaviourNode
    {
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
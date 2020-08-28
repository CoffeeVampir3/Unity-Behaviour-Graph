using Coffee.BehaviourTree.Context;
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
            Running,
            Waiting
        }
        
        protected TreeBaseNode(BehaviourTree tree)
        {
            parentTree = tree;
        }

        public abstract Result Execute(ref BehaviourContext context);

        public abstract void Reset();
    }
}
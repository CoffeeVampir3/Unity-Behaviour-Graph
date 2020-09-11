using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    internal abstract class TreeBaseNode
    {
        [HideInInspector]
        public BehaviourTree parentTree;
        public ContextBlock contextBlock;
        
        public enum Result
        {
            Failure,
            Success,
            Running
        }

        public abstract Result Execute();

        public abstract void Reset();
        
        protected TreeBaseNode(BehaviourTree tree)
        {
            parentTree = tree;
        }
    }
}
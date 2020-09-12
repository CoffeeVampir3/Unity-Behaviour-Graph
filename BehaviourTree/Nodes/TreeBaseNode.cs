using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    internal abstract class TreeBaseNode
    {
        [HideInInspector]
        public BehaviourTree parentTree;
        public Context context;
        
        public enum Result
        {
            Failure,
            Success,
            Running
        }

        public abstract Result Execute();

        public abstract void Reset();
        
        public TreeBaseNode(BehaviourTree tree, Context parentCtx)
        {
            parentTree = tree;
            context = parentCtx;
        }
    }
}
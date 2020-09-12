using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    /// <summary>
    /// Abstract base class for all tree nodes.
    /// </summary>
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

        /// <summary>
        /// Runs the logic associated with the node type.
        /// </summary>
        /// <returns>Whether the node completed successfully, failed, or is currently running.</returns>
        public abstract Result Execute();

        /// <summary>
        /// Resets the nodes state.
        /// </summary>
        public abstract void Reset();

        protected TreeBaseNode(BehaviourTree tree, Context parentCtx)
        {
            parentTree = tree;
            context = parentCtx;
        }
    }
}
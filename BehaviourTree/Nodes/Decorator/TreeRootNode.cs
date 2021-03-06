﻿using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeRootNode : TreeDecoratorNode
    {
        /// <summary>
        /// The root of the tree and the owner of the context root.
        /// </summary>
        /// <returns>
        /// Always returns the child value.
        /// </returns>
        public override Result Execute()
        {
            return child.Execute();
        }

        public override void Reset()
        {
            Debug.Assert(child != null);
            child.Reset();
        }

        public TreeRootNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
            context = new Context(null, this);
        }
    }
}
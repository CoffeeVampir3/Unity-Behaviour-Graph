﻿using Coffee.BehaviourTree.Context;
using Coffee.BehaviourTree.Ctx;
using UnityEngine;

namespace Coffee.BehaviourTree
{
    internal abstract class TreeBaseNode : ITreeBehaviourNode
    {
        [HideInInspector]
        public BehaviourTree parentTree;
        //public ContextBlock contextBlock;
        
        public enum Result
        {
            Failure,
            Success,
            Running
        }

        public abstract Result Execute(ref BehaviourContext context);

        public abstract void Reset();
        
        protected TreeBaseNode(BehaviourTree tree)
        {
            parentTree = tree;
        }
    }
}
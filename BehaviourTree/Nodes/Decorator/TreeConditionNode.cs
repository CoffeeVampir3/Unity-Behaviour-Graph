﻿using BehaviourGraph.Blackboard;
using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    internal class TreeConditionNode : TreeDecoratorNode
    {
        public RuntimeCondition runtimeCondition;

        public override Result Execute(ref BehaviourContext context)
        {
            if (runtimeCondition.Evaluate())
                return child.Execute(ref context);
            
            return Result.Failure;
        }

        public override void Reset()
        {
            Debug.Assert(child != null);
            Debug.Assert(runtimeCondition != null);
            child.Reset();
        }
        
        public TreeConditionNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}
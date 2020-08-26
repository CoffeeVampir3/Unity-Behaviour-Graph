using System;
using BehaviourGraph.Blackboard;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    [Serializable]
    [ShowOdinSerializedPropertiesInInspector]
    public class TreeConditionNode : TreeDecoratorNode
    {
        [NonSerialized, OdinSerialize]
        public BlackboardReference reference;
        public TreeConditionNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute()
        {
            Debug.Log("????");
            if (child == null)
                Debug.Log("Null child");
            if (reference == null)
            {
                Debug.Log("Failed to execute, null");
                return Result.Failure;
            }

            if (reference.Evaluate())
                return child.Execute();
            
            Debug.Log("Eval false.");
            return Result.Failure;
        }

        public override void Reset()
        {
            child?.Reset();
        }
    }
}
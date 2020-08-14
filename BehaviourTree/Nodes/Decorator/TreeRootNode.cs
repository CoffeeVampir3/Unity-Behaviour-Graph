using System;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    [Serializable]
    public class TreeRootNode : TreeDecoratorNode
    {
        public override Result Execute()
        {
            Debug.Log("Root executing.");

            if (child == null)
            {
                Debug.Log("No child for root.");
                return Result.Failure;
            }

            return child.Execute();
        }

        public TreeRootNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}
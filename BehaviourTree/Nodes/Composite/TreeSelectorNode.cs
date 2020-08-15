//Selector returns success if any children were successful.

using System;
using UnityEngine;

namespace Coffee.BehaviourTree.Composite
{
    [Serializable]
    public class TreeSelectorNode : TreeCompositeNode
    {
        public TreeSelectorNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute()
        {
            if (currentNode < childNodes.Length)
            {
                var result = childNodes[currentNode].Execute();

                switch (result)
                {
                    case Result.Running:
                        return result;
                    case Result.Success:
                        currentNode = 0;
                        return result;
                }
                
                currentNode++;
                if (currentNode < childNodes.Length)
                    return Result.Running;
            }

            currentNode = 0;
            return Result.Failure;
        }
    }
}
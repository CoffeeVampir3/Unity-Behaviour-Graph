using Coffee.BehaviourTree.Context;
using UnityEngine;

namespace Coffee.BehaviourTree.Composite
{
    //Selector returns success if any children were successful.
    internal class TreeSelectorNode : TreeCompositeNode
    {
        public TreeSelectorNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute(ref BehaviourContext context)
        {
            if (currentNode < childNodes.Length)
            {
                context.SetContext(this, Result.Running);
                var result = childNodes[currentNode].Execute(ref context);
                
                switch (result)
                {
                    case Result.Waiting:
                        Debug.Log("yote");
                        return result;
                    case Result.Running:
                        Debug.Log("4");
                        context.SetContext(this, Result.Running);
                        return result;
                    case Result.Success:
                        Debug.Log("6");
                        currentNode++;
                        context.SetContext(this, Result.Running);
                        return result;
                }
                
                currentNode++;
                if (currentNode < childNodes.Length)
                {
                    Debug.Log("5");
                    context.SetContext(this, Result.Running);
                    return Result.Running;
                }
            }

            Debug.Log("kay");
            currentNode = 0;
            context.Reset();
            return Result.Failure;
        }
    }
}
using Coffee.BehaviourTree.Context;

namespace Coffee.BehaviourTree.Composite
{
    //Selector returns success if any children were successful.
    internal class TreeSelectorNode : TreeCompositeNode
    {
        public override Result Execute(ref BehaviourContext context)
        {
            if (currentNode < childNodes.Length)
            {
                var result = childNodes[currentNode].Execute(ref context);
                
                switch (result)
                {
                    case Result.Waiting:
                        context.SetContext(this, Result.Waiting);
                        return result;
                    case Result.Running:
                        context.SetContext(this, Result.Running);
                        return result;
                    case Result.Success:
                        currentNode = 0;
                        context.Reset();
                        return result;
                }
                
                currentNode++;
                if (currentNode < childNodes.Length)
                {
                    context.SetContext(this, Result.Running);
                    return Result.Running;
                }
            }
            
            currentNode = 0;
            context.Reset();
            return Result.Failure;
        }
        
        public TreeSelectorNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}
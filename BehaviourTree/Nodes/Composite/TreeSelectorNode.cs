using Coffee.BehaviourTree.Context;

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
                var result = childNodes[currentNode].Execute(ref context);

                switch (result)
                {
                    case Result.Running:
                        context = new BehaviourContext(this, Result.Running);
                        return result;
                    case Result.Success:
                        currentNode = 0;
                        context.Reset();
                        return result;
                }
                
                currentNode++;
                if (currentNode < childNodes.Length)
                {
                    context = new BehaviourContext(this, Result.Running);
                    return Result.Running;
                }
            }

            currentNode = 0;
            context.Reset();
            return Result.Failure;
        }
    }
}
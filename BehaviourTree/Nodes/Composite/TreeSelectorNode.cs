

using Coffee.BehaviourTree.Ctx;

namespace Coffee.BehaviourTree.Composite
{
    //Selector returns success if any children were successful.
    internal class TreeSelectorNode : TreeCompositeNode
    {
        public override Result Execute()
        {
            for (; currentNode < childNodes.Length; currentNode++)
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
            }
            
            currentNode = 0;
            return Result.Failure;
        }
        
        public TreeSelectorNode(BehaviourTree tree, Context parentCtx) : 
            base(tree, parentCtx)
        {
        }
    }
}
using Coffee.BehaviourTree.Context;

namespace Coffee.BehaviourTree.Composite
{
    //Sequencer returns success if and only if ALL children were successful.
    internal class TreeSequencerNode : TreeCompositeNode
    {
        public TreeSequencerNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute(ref BehaviourContext context)
        {
            if (currentNode < childNodes.Length)
            {
                var result = childNodes[currentNode].Execute(ref context);

                switch (result)
                {
                    case Result.Waiting:
                        return result;
                    case Result.Running:
                        context.SetContext(this, Result.Running);
                        return result;
                    case Result.Failure:
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
            return Result.Success;
        }
    }
}
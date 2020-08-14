//Sequencer returns success if and only if ALL children were successful.

namespace Coffee.BehaviourTree.Composite
{
    public class TreeSequencerNode : TreeCompositeNode
    {
        private int currentNode = 0;
        
        public TreeSequencerNode(BehaviourTree tree) : base(tree)
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
                    case Result.Failure:
                        currentNode = 0;
                        return result;
                }
                
                currentNode++;
                if (currentNode < childNodes.Length)
                    return Result.Running;
            }

            currentNode = 0;
            return Result.Success;
        }
    }
}
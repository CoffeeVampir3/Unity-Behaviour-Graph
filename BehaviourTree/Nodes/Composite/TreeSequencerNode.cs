
namespace Coffee.BehaviourTree.Composite
{
    //Sequencer returns success if and only if ALL children were successful.
    internal class TreeSequencerNode : TreeCompositeNode
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

                    case Result.Failure:
                        currentNode = 0;
                        return result;
                }
            }
            
            currentNode = 0;
            return Result.Success;
        }
        
        public TreeSequencerNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}
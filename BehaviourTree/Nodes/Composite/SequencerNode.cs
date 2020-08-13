//Sequencer returns success if and only if ALL children were successful.

namespace Coffee.BehaviourTree.Composite
{
    public class SequencerNode : CompositeNode
    {
        private int currentNode = 0;
        public SequencerNode(BehaviourTree tree, IBehaviourNode[] children) : base(tree, children)
        {
        }

        public override BehaviourTree.Result Execute()
        {
            if (currentNode < childNodes.Length)
            {
                var result = childNodes[currentNode].Execute();

                switch (result)
                {
                    case BehaviourTree.Result.Running:
                        return result;
                    case BehaviourTree.Result.Failure:
                        currentNode = 0;
                        return result;
                }
                
                currentNode++;
                if (currentNode < childNodes.Length)
                    return BehaviourTree.Result.Running;
            }

            currentNode = 0;
            return BehaviourTree.Result.Success;
        }
        
    }
}
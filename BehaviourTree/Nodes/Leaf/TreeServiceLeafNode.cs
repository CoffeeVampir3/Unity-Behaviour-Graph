using System.Reflection;
using BehaviourGraph.Services;
using Coffee.BehaviourTree.Context;

namespace Coffee.BehaviourTree.Leaf
{
    internal class TreeServiceLeafNode : TreeLeafNode
    {
        public MethodInfo targetMethod;
        private RuntimeService rtService;

        public override Result Execute(ref BehaviourContext context)
        {
            if (rtService.Execute())
            {
                return Result.Waiting;
            }
            
            return Result.Success;
        }

        public override void Reset()
        {
            if (rtService == null)
            {
                rtService = new RuntimeService(targetMethod, parentTree.owner);
            }
        }
        
        public TreeServiceLeafNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}
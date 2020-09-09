using System.Reflection;
using BehaviourGraph.Services;
using Coffee.BehaviourTree.Context;

namespace Coffee.BehaviourTree.Leaf
{
    internal class TreeServiceLeafNode : TreeLeafNode
    {
        public MethodInfo targetMethod;
        private RuntimeService rtService;
        public TreeServiceLeafNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute(ref BehaviourContext context)
        {
            if (rtService.Execute())
            {
                context = new BehaviourContext(this, Result.Waiting);
                return Result.Running;
            }

            context.Reset();
            return Result.Success;
        }

        public override void Reset()
        {
            if (rtService == null)
            {
                rtService = new RuntimeService();
                rtService.Initialize(targetMethod, parentTree.owner);
            }
        }
    }
}
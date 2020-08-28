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

        ServiceCoroutineExtension.CoroutineController controller;

        public override Result Execute(ref BehaviourContext context)
        {
            if (controller == null)
            {
                ServiceCoroutineExtension.CoroutineHelper.Instance.StartCoroutineEx(
                    rtService.executable(parentTree.owner), out controller);
                
                if(controller.state == ServiceCoroutineExtension.CoroutineState.Ready)
                    controller.Start();
            }
            if (controller.state == ServiceCoroutineExtension.CoroutineState.Running)
            {
                context = new BehaviourContext(this, Result.Waiting);
                return Result.Waiting;
            }
            
            context.Reset();
            controller = null;
            return Result.Success;
        }

        public override void Reset()
        {
            controller = null;

            if (rtService == null)
            {
                rtService = new RuntimeService();
                rtService.Initialize(targetMethod, parentTree.owner);
            }
        }
    }
}
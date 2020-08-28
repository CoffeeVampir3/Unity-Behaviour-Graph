using System.Reflection;
using BehaviourGraph.Services;
using Coffee.BehaviourTree.Context;
using UnityEngine;

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
            if (rtService.executable.Invoke(parentTree.owner) != null)
            {
                context = new BehaviourContext(this, Result.Running);
                return Result.Running;
            }

            context.Reset();
            return Result.Success;
        }

        public override void Reset()
        {
            if (targetMethod == null)
            {
                Debug.Log("Null method.");
            }
            if (parentTree.owner == null)
            {
                Debug.Log("Null owner.");
            }
            rtService = new RuntimeService();
            rtService.Initialize(targetMethod, parentTree.owner);
        }
    }
}
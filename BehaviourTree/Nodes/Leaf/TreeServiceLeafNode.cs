using System;
using System.Collections;
using System.Reflection;
using BehaviourGraph.Services;
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

        public override Result Execute()
        {
            if (rtService.executable.Invoke(parentTree.owner) != null)
            {
                return Result.Running;
            }
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
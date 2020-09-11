﻿using System.Reflection;
using BehaviourGraph.Services;
using Coffee.BehaviourTree.Context;
using UnityEngine;

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
                return Result.Running;
            }
            
            return Result.Success;
        }

        public override void Reset()
        {
            if (rtService == null)
            {
                rtService = new RuntimeService(targetMethod, parentTree.owner);
            }
            Debug.Assert(rtService != null);
        }
        
        public TreeServiceLeafNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}
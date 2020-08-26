using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.BehaviourTree.Composite
{
    [Serializable]
    [ShowOdinSerializedPropertiesInInspector]
    internal abstract class TreeCompositeNode : TreeBaseNode
    {
        [NonSerialized, OdinSerialize]
        public TreeBaseNode[] childNodes;
        [NonSerialized, OdinSerialize]
        protected int currentNode = 0;

        public void SetChildren(List<TreeBaseNode> nodes)
        {
            childNodes = nodes.ToArray();
        }

        protected TreeCompositeNode(BehaviourTree tree) : base(tree)
        {
        }

        public override void Reset()
        {
            currentNode = 0;
            for (int i = childNodes.Length - 1; i >= 0; i--)
            {
                childNodes[i].Reset();
            }
        }
    }
}
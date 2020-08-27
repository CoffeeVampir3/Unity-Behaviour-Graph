using System;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    [Serializable]
    internal abstract class LeafNode : BaseNode
    {
        [SerializeField]
        [InputAttribute(ShowBackingValue.Never)] public BaseNode[] parents = null;
    }
}
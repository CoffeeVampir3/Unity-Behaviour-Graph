using System;
using Coffee.Behaviour.Nodes.Private;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    [Serializable]
    internal abstract class DecoratorNode : RootDecoratorNode
    {
        [SerializeField]
        [InputAttribute(ShowBackingValue.Never)] public BaseNode[] parents = null;
    }
}
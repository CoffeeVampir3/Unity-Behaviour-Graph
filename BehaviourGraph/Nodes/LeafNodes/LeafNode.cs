using UnityEngine;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    public abstract class LeafNode : BaseNode
    {
        [SerializeField]
        [InputAttribute(ShowBackingValue.Never)] public BaseNode[] parents;
    }
}
using System;
using System.Reflection;
using BehaviourGraph.Attributes;
using BehaviourGraph.CodeLinks.AttributeCache;
using Coffee.BehaviourTree;
using Coffee.BehaviourTree.Leaf;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    internal class ServiceLeafNode : LeafNode
    {
        [SerializeField]
        [ValueDropdown("GetServices", NumberOfItemsBeforeEnablingSearch = 2)]
        public string targetMethod = null;
        public ValueDropdownList<string> GetServices => AttributeCache<Service>.GetCachedMemberDropdown();
        
        [NonSerialized, OdinSerialize, HideInInspector]
        protected  TreeServiceLeafNode leafNode;
        
        protected override void OnCreation()
        {
            leafNode = new TreeServiceLeafNode(null);
            thisTreeNode = leafNode;
        }

        public override TreeBaseNode WalkGraphToCreateTree(BehaviourTree.BehaviourTree tree)
        {
            if (AttributeCache<Service>.TryGetCachedMemberViaLookupValue(targetMethod, 
                    out var method))
            {
                var node = new TreeServiceLeafNode(tree) {targetMethod = method as MethodInfo};
                return node;
            }

            Debug.LogError("Unable to recover cached member lookup value for service: " + targetMethod);
            return null;
        }
    }
}
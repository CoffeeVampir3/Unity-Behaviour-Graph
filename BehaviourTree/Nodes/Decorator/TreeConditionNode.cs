using System;
using BehaviourGraph.Conditionals;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Coffee.BehaviourTree.Decorator
{
    [Serializable]
    public class TreeConditionNode : TreeDecoratorNode
    {
        [NonSerialized, OdinSerialize]
        [ValueDropdown("GetFilteredTypeList")]
        public Type typeSelector;

        private ValueDropdownList<Type> GetFilteredTypeList =>
            ConditionalCache.GetDropdownListOfClassesWithConditions();
        
        public TreeConditionNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}
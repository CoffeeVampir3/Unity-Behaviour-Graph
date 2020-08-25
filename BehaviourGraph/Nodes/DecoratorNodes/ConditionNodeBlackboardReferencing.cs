using BehaviourGraph.Blackboard;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Coffee.Behaviour.Nodes.DecoratorNodes
{
    public partial class ConditionNode
    {
        #if UNITY_EDITOR
        [ValueDropdown("GetBlackboardConditions", DropdownHeight = 500, DropdownWidth = 500, NumberOfItemsBeforeEnablingSearch = 2)]
        #endif
        [OdinSerialize]
        protected BlackboardReference blackboardReferenceTarget;
        
        #if UNITY_EDITOR
        protected ValueDropdownList<BlackboardReference> GetBlackboardConditions()
        {
            if(parentGraph != null)
                return parentGraph.GetAllBlackboardReferences();

            return null;
        }
        #endif
    }
}
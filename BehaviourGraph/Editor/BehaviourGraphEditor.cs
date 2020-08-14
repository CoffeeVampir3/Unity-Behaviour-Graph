using UnityEngine;
using XNode;
using XNodeEditor;

namespace Coffee.Behaviour.Editor
{
    [CustomNodeGraphEditor(typeof(BehaviourGraph))]
    public class BehaviourGraphEditor : NodeGraphEditor
    {
        public override string GetNodeMenuName(System.Type type) {
            if (type.Namespace.Contains("Coffee.Behaviour.Nodes.Private"))
            {
                return null;
            }
            
            if (type.Namespace.Contains("Coffee.Behaviour.Nodes"))
            {
                return base.GetNodeMenuName(type).Replace("Coffee/Behaviour/Nodes/", "Nodes/");
            }

            return null;
        }

        public override void OnCreate()
        {
            (target as BehaviourGraph).Init();
            base.OnCreate();
        }
    }
}
using XNodeEditor;
using Debug = System.Diagnostics.Debug;

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
            BehaviourGraph bg = target as BehaviourGraph;

            Debug.Assert(bg != null, nameof(bg) + " != null");
            bg.EditorTimeInitialization();
            base.OnCreate();
        }
    }
}
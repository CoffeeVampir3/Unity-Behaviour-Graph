using XNode;
using XNodeEditor;

namespace Coffee.Behaviour.Editor
{
    [CustomNodeGraphEditor(typeof(BehaviourGraph))]
    public class BehaviourGraphEditor : NodeGraphEditor
    {
        private bool isDirty = false;

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

        public void SetDirty()
        {
            isDirty = true;
        }

        public override void OnGUI()
        {
            base.OnGUI();
            BehaviourGraph graph = target as BehaviourGraph;
            graph.Init();

            if(isDirty)
                window.Repaint();
        }

    }
}
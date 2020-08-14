using XNode;
using XNodeEditor;

namespace Coffee.Behaviour.Editor
{
    [CustomNodeGraphEditor(typeof(BehaviourGraph))]
    public class BehaviourGraphEditor : NodeGraphEditor
    {
        private bool isDirty = false;

        public override string GetNodeMenuName(System.Type type) {
            if (type.Namespace.Contains("Coffee.BehaviourGraph"))
            {
                return base.GetNodeMenuName(type).Replace("BehaviourGraph/", "");
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
            if(isDirty)
                window.Repaint();
        }

    }
}
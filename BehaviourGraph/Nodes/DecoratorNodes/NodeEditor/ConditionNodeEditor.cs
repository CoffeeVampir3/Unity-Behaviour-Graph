using Coffee.Behaviour.Nodes.DecoratorNodes;
using XNodeEditor;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    [NodeEditor.CustomNodeEditorAttribute(typeof(ConditionNode))]
    public class ConditionNodeEditor : NodeEditor
    {
        public override int GetWidth() {
            return 350;
        }
    }
}
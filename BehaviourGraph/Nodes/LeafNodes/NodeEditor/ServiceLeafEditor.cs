using UnityEngine;
using XNodeEditor;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    [NodeEditor.CustomNodeEditorAttribute(typeof(LeafNode))]
    public class ServiceLeafEditor : NodeEditor
    {
        public override int GetWidth() {
            return 350;
        }

        private static Color GuiColor = new Color(70/255f, 73/255f, 76/255f, 220/255f);
        public override Color GetTint()
        {
            return GuiColor;
        }
    }
}
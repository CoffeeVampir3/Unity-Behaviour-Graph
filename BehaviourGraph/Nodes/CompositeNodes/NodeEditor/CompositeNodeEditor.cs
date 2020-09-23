using Coffee.Behaviour.Nodes.CompositeNodes;
using UnityEngine;
using XNodeEditor;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    [NodeEditor.CustomNodeEditorAttribute(typeof(CompositeNode))]
    public class CompositeNodeEditor : NodeEditor
    {
        private static Color GuiColor = new Color(60/255f, 70/255f, 88/255f, 220/255f);
        public override Color GetTint()
        {
            return GuiColor;
        }
    }
}
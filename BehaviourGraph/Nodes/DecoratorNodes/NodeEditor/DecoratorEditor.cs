#if UNITY_EDITOR

using Coffee.Behaviour.Nodes.DecoratorNodes;
using UnityEngine;
using XNodeEditor;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    [NodeEditor.CustomNodeEditorAttribute(typeof(DecoratorNode))]
    public class DecoratorEditor : NodeEditor
    {
        private static Color GuiColor = new Color(76/255f, 92/255f, 104/255f, 220/255f);
        public override Color GetTint()
        {
            return GuiColor;
        }
    }
}

#endif
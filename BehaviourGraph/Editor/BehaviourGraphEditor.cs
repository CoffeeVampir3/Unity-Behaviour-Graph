using XNodeEditor;
using Debug = System.Diagnostics.Debug;

namespace Coffee.Behaviour.Editor
{
    [CustomNodeGraphEditor(typeof(BehaviourGraph))]
    public class BehaviourGraphEditor : NodeGraphEditor
    {
        /// <summary>
        /// Sets up our nodes within our namespace to be automatically added to the context
        /// menu.
        /// <para>If you want to add your own extension nodes you must append the namespace here.</para>
        /// </summary>
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

        /// <summary>
        /// A bootstrap hack to create a root node on creation.
        /// </summary>
        public override void OnCreate()
        {
            BehaviourGraph bg = target as BehaviourGraph;

            Debug.Assert(bg != null, nameof(bg) + " != null");
            bg.EditorTimeInitialization();
            base.OnCreate();
        }
    }
}
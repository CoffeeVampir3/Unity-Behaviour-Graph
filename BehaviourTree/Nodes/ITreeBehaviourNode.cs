using Coffee.BehaviourTree.Context;

namespace Coffee.BehaviourTree
{
    internal interface ITreeBehaviourNode
    {
        TreeBaseNode.Result Execute(ref BehaviourContext context);
        void Reset();
    }
}
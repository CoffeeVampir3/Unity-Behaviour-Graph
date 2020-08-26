namespace Coffee.BehaviourTree
{
    internal interface ITreeBehaviourNode
    {
        TreeBaseNode.Result Execute();
        void Reset();
    }
}
namespace Coffee.BehaviourTree
{
    public interface ITreeBehaviourNode
    {
        TreeBaseNode.Result Execute();
    }
}
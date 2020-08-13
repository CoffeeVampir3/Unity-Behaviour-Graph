namespace Coffee.BehaviourTree
{
    public interface IBehaviourNode
    {
        BaseNode.Result Execute();
    }
}
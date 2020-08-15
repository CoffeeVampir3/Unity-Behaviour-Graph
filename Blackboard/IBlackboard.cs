namespace BehaviourGraph.Blackboard
{
    public interface IBlackboard
    {
        T GetItem<T>(int index);
        int SetItem<T>(T item);

        void ReleaseId(int id);
    }
}
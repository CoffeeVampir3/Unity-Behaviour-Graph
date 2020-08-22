using System;

namespace BehaviourGraph.Blackboard.Deprecated
{
    public interface IBlackboard
    {
        T GetItem<T>(int index);
        object GetItem(int index);
        Type GetType(int index);
        int SetItem<T>(T item);

        void Initialize(int allocationSize);

        void ReleaseId(int id);
    }
}
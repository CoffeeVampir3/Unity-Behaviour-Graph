using System.Reflection;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    public interface ISerializedMemberInfo
    {
        MemberInfo Get();
        void Set(MemberInfo item);
    }
}
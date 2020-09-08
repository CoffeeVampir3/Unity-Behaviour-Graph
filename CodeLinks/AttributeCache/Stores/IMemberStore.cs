using System.Reflection;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    public interface IMemberStore
    {
        MemberInfo GetMemberByHash(int hash);
    }
}
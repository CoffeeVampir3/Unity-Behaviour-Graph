using System;
using System.Linq;
using System.Reflection;
using BehaviourGraph.CodeLinks;
using BehaviourGraph.CodeLinks.AttributeCache;
using BehaviourGraph.Conditionals;
using BehaviourGraph.Services;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using Expression = System.Linq.Expressions.Expression;

namespace BehaviourGraph.Debugging
{
    public class CacheDebugger : SerializedMonoBehaviour
    {

        [NonSerialized, OdinSerialize] 
        private BehaviourGraphSMS store;

        [Button]
        public void Path()
        {
            MonoScript thisScript = MonoScript.FromMonoBehaviour(this);
            Debug.Log(AssetDatabase.GetAssetPath(thisScript));
        }
        
        [Button]
        public void DebugFis()
        {
        }

        [SerializeField, ValueDropdown("GetConditionMembers")] 
        public string conditionSelector;
        public ValueDropdownList<string> GetConditionMembers => 
            AtribCache<Condition>.GetCachedMemberDropdown();
        
        [SerializeField, ValueDropdown("GetServiceMembers")] 
        public string serviceSelector;
        public ValueDropdownList<string> GetServiceMembers => 
            AtribCache<Service>.GetCachedMemberDropdown();

        [Button]
        public void Dosdoifh()
        {
            var q = this.GetType();
            var alp = q.GetMethods(
                BindingFlags.NonPublic | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase |
                BindingFlags.Static | BindingFlags.InvokeMethod);
            
            foreach (var x in alp)
            {
                Debug.Log(x.Name);
            }
        }

        public void MakeCache()
        {
            AttributeCacheRetainer.CacheOrGetCachedAttributeData<FieldInfo, Condition>(
                out var conditionalFieldsDict);

            AttributeCacheRetainer.CacheOrGetCachedAttributeData<MethodInfo, Condition>(
                out var conditionalMethodsDict);

            AttributeCacheRetainer.CacheOrGetCachedAttributeData<MethodInfo, Service>(
                out var serviceMethodsDict);
        }


        public void DebugRuntime()
        {
            var a = AttributeCacheRetainer.
                CacheOrGetCachedAttributeData<FieldInfo, Condition>(
                out var conditionalFieldsDict);

            var b = AttributeCacheRetainer.
                CacheOrGetCachedAttributeData<MethodInfo, Condition>(
                out var conditionalMethodsDict);

            var c = AttributeCacheRetainer.
                CacheOrGetCachedAttributeData<MethodInfo, Service>(
                out var serviceMethodsDict);

            foreach (var l in a)
            {
                Debug.Log(l.Name);
            }
            foreach (var l in b)
            {
                Debug.Log(l.Name);
            }
            foreach (var l in c)
            {
                Debug.Log(l.Name);
            }
        }
        
        private Func<bool> CreateConditionFunction(MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType = Expression.GetFuncType;
            var types = new[] {methodInfo.ReturnType};

            return (Func<bool>)Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
        
    }
}
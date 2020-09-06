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
        private AttributeCache cache;

        [Button]
        public void DebugFis()
        {
            if (cache == null)
            {
                cache = ScriptableObject.CreateInstance<AttributeCache>();
                cache.name = "CBG_Cache";
                AssetDatabase.CreateAsset(cache, @"Assets\!Tests\" + cache.name + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(cache));
                AssetDatabase.Refresh();
            }

            var fCond = TypeCache.GetFieldsWithAttribute<Condition>();
            cache.Cache<Condition>(fCond.ToList());
            
            var mCond = TypeCache.GetMethodsWithAttribute<Condition>();
            cache.Cache<Condition>(mCond.ToList());
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
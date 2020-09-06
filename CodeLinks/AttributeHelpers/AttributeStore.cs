using System;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

// Mono ~ Mono why would you serialize
// ~ Mono ~ Mono ~ 
//...
//...
//God is dead.

namespace BehaviourGraph.CodeLinks
{
    internal abstract class AttributeStore : SerializedScriptableObject
    {
        protected abstract void OnCreated<CachingItem>(ref CachingItem[] members)
            where CachingItem : MemberInfo;

        [NonSerialized, OdinSerialize, HideInInspector]
        public Type cachedAttributeType = null;
        public abstract MemberInfo[] Retrieve();

        #if UNITY_EDITOR
        protected static StoreType CreateOrStore<StoreType, CachingItem, Attr>(ref CachingItem[] members) 
            where StoreType : AttributeStore
            where CachingItem : MemberInfo
            where Attr : Attribute
        {
            var container = AttributeCacheRetainer.GetStoreContainer();
            var q = container.GetAttributeStoresOfType<StoreType, Attr>();
            if (q != null && q.Count > 0)
            {
                return q[0];
            }

            StoreType store = CreateInstance<StoreType>();
            store.name = typeof(StoreType).Name + "_" + typeof(Attr).Name;
            store.cachedAttributeType = typeof(Attr);
            store.OnCreated(ref members);

            AssetDatabase.AddObjectToAsset(store, container);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return store;
        }
        #endif
        
    }
}
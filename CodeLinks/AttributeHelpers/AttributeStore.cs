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

namespace BehaviourGraph
{
    public abstract class AttributeStore : SerializedScriptableObject
    {
        protected abstract void OnCreated<CachingItem>(ref CachingItem[] members)
            where CachingItem : MemberInfo;

        [NonSerialized, OdinSerialize, HideInInspector]
        public Type cachedAttributeType;
        public abstract MemberInfo[] Retrieve();

        protected static StoreType CreateOrStore<StoreType, CachingItem, Attr>(ref CachingItem[] members) 
            where StoreType : AttributeStore
            where CachingItem : MemberInfo
            where Attr : Attribute
        {
            StoreType store = CreateInstance<StoreType>();
            store.name = typeof(StoreType).Name + "_" + typeof(Attr).Name;
            store.cachedAttributeType = typeof(Attr);
            store.OnCreated(ref members);

            var container = AttributeCacheRetainer.GetStoreContainer();
            
            AssetDatabase.AddObjectToAsset(store, container);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return store;
        }
        
    }
}
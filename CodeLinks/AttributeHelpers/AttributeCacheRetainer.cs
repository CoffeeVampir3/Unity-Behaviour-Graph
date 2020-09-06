using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BehaviourGraph.CodeLinks
{
    public static partial class AttributeCacheRetainer
    {
        internal static DataStoreContainer GetStoreContainer()
        {
#if UNITY_EDITOR
            return GetEditorTimeStorageContainer();
#else
            return GetRuntimeStoreContainer();
#endif
        }
        
        public static CachingItem[] CacheOrGetCachedAttributeData<CachingItem, Attr>(
            out Dictionary<(Type, Type), CachingItem[]> cacheDictionary) 
            where CachingItem : MemberInfo 
            where Attr : Attribute
        {
#if UNITY_EDITOR
            return EditorTimeReflectAndCache<CachingItem, Attr>(
                out cacheDictionary);
#else
            return RuntimeGetFromCache<CachingItem, Attr>(
                out cacheDictionary);
#endif
        }
    }
}
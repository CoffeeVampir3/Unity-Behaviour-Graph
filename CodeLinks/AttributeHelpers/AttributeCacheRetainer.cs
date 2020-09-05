using System;
using System.Collections.Generic;
using System.Reflection;

namespace BehaviourGraph
{
    public static partial class AttributeCacheRetainer
    {
        public static readonly List<FieldAttributeStore> fieldStores = new List<FieldAttributeStore>();
        public static readonly List<MethodAttributeStore> methodStores = new List<MethodAttributeStore>();

        public static DataStoreContainer GetStoreContainer()
        {
#if UNITY_EDITOR
            return GetEditorTimeStorageContainer();
#else
            return GetRuntimeStoreContainer();
#endif
        }
        
        public static CachingItem[] CacheOrGetCachedAttributeData<CachingItem, Attr>(
            CachingItem[] itemSelection,
            out Dictionary<(Type, Type), CachingItem[]> cacheDictionary) 
            where CachingItem : MemberInfo 
            where Attr : Attribute
        {
#if UNITY_EDITOR
            return EditorTimeReflectAndCache<CachingItem, Attr>(
                itemSelection,
                out cacheDictionary);
#else
            return RuntimeGetFromCache<CachingItem, Attr>(
                itemSelection,
                out cacheDictionary);
#endif
        }
    }
}
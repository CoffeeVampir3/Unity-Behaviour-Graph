using System;
using System.Collections.Generic;
using System.Reflection;

namespace BehaviourGraph
{
    public static partial class AttributeCacheRetainer
    {
        private static List<FieldAttributeStore> fieldStores;
        private static List<MethodAttributeStore> methodStores;

        public static DataStoreContainer GetStoreContainer()
        {
#if UNITY_EDITOR
            return GetEditorTimeStorageContainer();
#else
            return GetRuntimeStoreContainer();
#endif
        }

        public static CachingItem[] CacheOrGetCachedAttributeData<CachingItem, Attr>(
            ref Dictionary<(Type, Type), CachingItem[]> cacheDictionary,
            ref List<Type> declaredTypes,
            ref CachingItem[] itemSelection)
            where CachingItem : MemberInfo
            where Attr : Attribute
        {
#if UNITY_EDITOR
            return EditorTimeReflectAndCache<CachingItem, Attr>(
                ref cacheDictionary,
                ref declaredTypes,
                ref itemSelection);
#else
            return null;
#endif
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BehaviourGraph
{
    public static partial class AttributeCacheRetainer
    {
        private static DataStoreContainer rtStoreContainer;
        private static bool initialized = false;

        private static DataStoreContainer GetRuntimeStoreContainer()
        {
            if (initialized)
            {
                return rtStoreContainer;
            }
            
            rtStoreContainer = GameObject.FindObjectOfType<DataStoreContainer>();
            if(rtStoreContainer == null)
                throw new Exception("Unity Behaviour Graph failed to find an attribute cache directory to build runtime values.");

            initialized = true;
            return rtStoreContainer;
        }
        
        public static CachingItem[] RuntimetimeGetFromCache<CachingItem, Attr>(
            ref Dictionary<(Type, Type), CachingItem[]> cacheDictionary,
            ref List<Type> declaredTypes,
            ref CachingItem[] itemSelection) 
            where CachingItem : MemberInfo 
            where Attr : Attribute
        {
            Type cachingType = typeof(CachingItem);
            
            CachingItem[] attributeData = AttributeCacheFactory.CacheMemberInfo<CachingItem, Attr>(
                ref cacheDictionary,
                ref declaredTypes,
                ref itemSelection);
            
            if (typeof(FieldInfo).IsAssignableFrom(cachingType))
            {
            }
            else if(typeof(MethodInfo).IsAssignableFrom(cachingType))
            {
            }
            return attributeData;
        }
    }
}
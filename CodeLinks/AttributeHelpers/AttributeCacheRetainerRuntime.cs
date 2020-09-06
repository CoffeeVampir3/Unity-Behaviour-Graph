using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BehaviourGraph.CodeLinks
{
    public static partial class AttributeCacheRetainer
    {
        private static DataStoreContainer rtStoreContainer = null;
        private static bool initialized = false;
        
        private static bool CacheExists()
        {
            if (initialized)
            {
                return true;
            }
            
            var dscs = Resources.FindObjectsOfTypeAll<DataStoreContainer>();
            if (dscs.Length > 0)
            {
                rtStoreContainer = dscs[0];
                initialized = true;
                return true;
            }

            return false;
        }

        private static DataStoreContainer GetRuntimeStoreContainer()
        {
            if (CacheExists())
                return rtStoreContainer;
            
            if(rtStoreContainer == null)
                throw new Exception("Unity Behaviour Graph failed to find an attribute cache directory to build runtime values.");
            
            return null;
        }
        
        internal static CachingItem[] RuntimeGetFromCache<CachingItem, Attr>(
            out Dictionary<(Type, Type), CachingItem[]> cacheDictionary) 
            where CachingItem : MemberInfo 
            where Attr : Attribute
        {
            Type cachingType = typeof(CachingItem);

            List<CachingItem> attributeData = new List<CachingItem>();
            cacheDictionary = new Dictionary<(Type, Type), CachingItem[]>();
            
            var rtStorage = GetRuntimeStoreContainer();
            if (typeof(FieldInfo).IsAssignableFrom(cachingType))
            {
                var stores = rtStorage.GetFieldAttributeStoresOfType<Attr>();

                for (int i = 0; i < stores.Count; i++)
                {
                    if (!(stores[i].Retrieve() is CachingItem[] items)) 
                        continue;
                    
                    attributeData.AddRange(items);
                    var dictionaryIndex = (stores[i].cachedAttributeType, typeof(Attr));
                    cacheDictionary.Add(dictionaryIndex, items);
                }
            }
            else if(typeof(MethodInfo).IsAssignableFrom(cachingType))
            {
                var stores = rtStorage.GetMethodAttributeStoresOfType<Attr>();

                for (int i = 0; i < stores.Count; i++)
                {
                    if (!(stores[i].Retrieve() is CachingItem[] items)) 
                        continue;
                    
                    attributeData.AddRange(items);
                    var dictionaryIndex = (stores[i].cachedAttributeType, typeof(Attr));
                    cacheDictionary.Add(dictionaryIndex, items);
                }
            }
            
            return attributeData.ToArray();
        }
    }
}
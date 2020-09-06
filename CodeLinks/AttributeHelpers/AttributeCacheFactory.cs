using System;
using System.Collections.Generic;
using System.Reflection;

namespace BehaviourGraph.CodeLinks
{
    internal static class AttributeCacheFactory
    {
        internal static CachingItem[] CacheMemberInfo<CachingItem, Attr>(
            CachingItem[] itemSelection,
            out Dictionary<(Type, Type), CachingItem[]> cacheDictionary)
            where CachingItem : MemberInfo
            where Attr : Attribute
        {
            cacheDictionary = new Dictionary<(Type, Type), CachingItem[]>();
            
            var tempCache = new Dictionary<(Type, Type), List<CachingItem>>();
            var allItems = new List<CachingItem>();
            var declaredTypes = new List<Type>();
            
            foreach (CachingItem item in itemSelection)
            {
                var dictionaryIndex = (item.DeclaringType, typeof(Attr));
                if (!tempCache.TryGetValue(dictionaryIndex, out var cachedItems))
                {
                    cachedItems = new List<CachingItem>();
                    tempCache.Add(dictionaryIndex, cachedItems);
                }

                if (!declaredTypes.Contains(item.DeclaringType))
                {
                    declaredTypes.Add(item.DeclaringType);
                }
                
                cachedItems.Add(item);
                allItems.Add(item);
            }

            foreach (var declaredType in declaredTypes)
            {
                var dictionaryIndex = (declaredType, typeof(Attr));
                tempCache.TryGetValue(dictionaryIndex, out var cachedItems);
                if(cachedItems != null)
                    cacheDictionary.Add(dictionaryIndex, cachedItems.ToArray());
            }
            
            return allItems.ToArray();
        }
    }
}
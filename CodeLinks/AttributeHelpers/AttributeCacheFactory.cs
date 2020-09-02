using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BehaviourGraph
{
    internal static class AttributeCacheFactory
    {
        internal static CachingItem[] CacheMemberInfo<CachingItem, Attr>(
            ref Dictionary<(Type, Type), CachingItem[]> cacheDictionary,
            ref List<Type> declaredTypes,
            ref CachingItem[] itemSelection) 
            where CachingItem : MemberInfo 
            where Attr : Attribute
        {
            if(cacheDictionary == null)
                cacheDictionary = new Dictionary<(Type, Type), CachingItem[]>();
            
            if(declaredTypes == null)
                declaredTypes = new List<Type>();
            
            var tempCache = new Dictionary<(Type, Type), List<CachingItem>>();
            List<CachingItem> allItems = new List<CachingItem>();
            
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
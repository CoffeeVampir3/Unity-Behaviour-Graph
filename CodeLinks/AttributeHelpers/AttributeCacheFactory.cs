using System;
using System.Collections.Generic;
using System.Reflection;

namespace BehaviourGraph
{
    internal static class AttributeCacheFactory
    {
        internal static CachingItem[] CacheMemberInfo<CachingItem, Attr>(
            ref Dictionary<(Type, Type), CachingItem[]> cacheDictionary, 
            ref CachingItem[] itemSelection,
            Action<CachingItem> perItemCallback) 
            where CachingItem : MemberInfo 
            where Attr : Attribute
        {
            cacheDictionary = new Dictionary<(Type, Type), CachingItem[]>();

            List<CachingItem> allItems = new List<CachingItem>();
            foreach (CachingItem item in itemSelection)
            {
                var dictionaryIndex = (item.DeclaringType, typeof(Attr));
                if (!cacheDictionary.TryGetValue(dictionaryIndex, out var cachedItems))
                {
                    cachedItems = new CachingItem[1];
                }
                else
                {
                    var temp = cachedItems;
                    cachedItems = new CachingItem[cachedItems.Length + 1];
                    temp.CopyTo(cachedItems, 0);
                }
                
                cachedItems[cachedItems.Length - 1] = item;
                cacheDictionary.Remove(dictionaryIndex);
                cacheDictionary.Add(dictionaryIndex, cachedItems);
                allItems.Add(item);

                perItemCallback?.Invoke(item);
            }

            return allItems.ToArray();
        }
    }
}
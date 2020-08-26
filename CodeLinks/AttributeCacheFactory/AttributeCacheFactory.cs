using System;
using System.Collections.Generic;
using System.Reflection;

namespace BehaviourGraph
{
    public static class AttributeCacheFactory
    {
        public static void CacheMemberInfo<CachingItem, Attr>(
            ref Dictionary<(Type, Type), CachingItem[]> cacheDictionary, CachingItem[] itemSelection,
            Action<CachingItem> perItemCallback) 
            where CachingItem : MemberInfo 
            where Attr : Attribute
        {
            cacheDictionary = new Dictionary<(Type, Type), CachingItem[]>();

            foreach (CachingItem item in itemSelection)
            {
                var dictionaryIndex = (item.DeclaringType, typeof(Attr));
                if (!cacheDictionary.TryGetValue(dictionaryIndex, out var info))
                {
                    info = new CachingItem[1];
                }
                else
                {
                    var temp = info;
                    info = new CachingItem[info.Length + 1];
                    temp.CopyTo(info, 0);
                }
                
                info[info.Length - 1] = item;
                cacheDictionary.Remove(dictionaryIndex);
                cacheDictionary.Add(dictionaryIndex, info);

                perItemCallback.Invoke(item);
            }
        }
    }
}
﻿#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.CodeLinks
{
    public static partial class AttributeCacheRetainer
    {
        private static DataStoreContainer storeContainer = null;
        private static DataStoreContainer GetEditorTimeStorageContainer()
        {
            if (storeContainer != null)
                return storeContainer;

            var dscs = Resources.FindObjectsOfTypeAll<DataStoreContainer>();
            if (dscs.Length > 0)
            {
                storeContainer = dscs[0];
                return storeContainer;
            }
                
            
            storeContainer = ScriptableObject.CreateInstance<DataStoreContainer>();
            storeContainer.name = "UBG Directory Locator";
            AssetDatabase.CreateAsset(storeContainer, @"Assets\!Tests\" + storeContainer.name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(storeContainer));
            AssetDatabase.Refresh();

            return storeContainer;
        }

        private static CachingItem[] EditorTimeReflectAndCache<CachingItem, Attr>(
            out Dictionary<(Type, Type), CachingItem[]> cacheDictionary) 
            where CachingItem : MemberInfo 
            where Attr : Attribute
        {
            Type cachingType = typeof(CachingItem);

            cacheDictionary = null;
            CachingItem[] attributeData = null;
            if (typeof(FieldInfo).IsAssignableFrom(cachingType))
            {
                FieldInfo[] itemSelection = TypeCache.GetFieldsWithAttribute<Attr>().ToArray();
                attributeData = AttributeCacheFactory.CacheMemberInfo<CachingItem, Attr>(
                    itemSelection as CachingItem[],
                    out cacheDictionary);
                
                var sc = GetEditorTimeStorageContainer();
                sc.StoreItem(
                    FieldAttributeStore.CreateOrStore<CachingItem, Attr>(ref attributeData));
            }
            else if(typeof(MethodInfo).IsAssignableFrom(cachingType))
            {   
                MethodInfo[] itemSelection = TypeCache.GetMethodsWithAttribute<Attr>().ToArray();
                attributeData = AttributeCacheFactory.CacheMemberInfo<CachingItem, Attr>(
                    itemSelection as CachingItem[],
                    out cacheDictionary);
                
                var sc = GetEditorTimeStorageContainer();
                sc.StoreItem(
                    MethodAttributeStore.CreateOrStore<CachingItem, Attr>(ref attributeData));
            }

            return attributeData;
        }
    }
}
#endif
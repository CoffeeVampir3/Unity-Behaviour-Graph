#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph
{
    public static partial class AttributeCacheRetainer
    {
        private static DataStoreContainer storeContainer;
        private static DataStoreContainer GetEditorTimeStorageContainer()
        {
            if (storeContainer != null)
                return storeContainer;
                    
            storeContainer = GameObject.FindObjectOfType<DataStoreContainer>();
            if (storeContainer == null)
            {
                storeContainer = ScriptableObject.CreateInstance<DataStoreContainer>();
                storeContainer.name = "UBG Directory Locator";
                AssetDatabase.CreateAsset(storeContainer, @"Assets\!Tests\" + storeContainer.name + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(storeContainer));
                AssetDatabase.Refresh();
            }

            return storeContainer;
        }
        
        public static CachingItem[] EditorTimeReflectAndCache<CachingItem, Attr>(
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
                FieldAttributeStore.CreateOrStore<CachingItem, Attr>(ref attributeData);
            }
            else if(typeof(MethodInfo).IsAssignableFrom(cachingType))
            {
                MethodAttributeStore.CreateOrStore<CachingItem, Attr>(ref attributeData);
            }

            return attributeData;
        }
    }
}
#endif
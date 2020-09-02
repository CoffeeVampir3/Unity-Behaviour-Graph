using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph
{
    public static partial class AttributeCacheRetainer
    {
        private static string GetDirectoryPathFromAsset(ScriptableObject obj)
        {
            string dirPath = AssetDatabase.GetAssetPath(obj);
            return dirPath.Substring(0, dirPath.LastIndexOf('/') + 1);
        }

#if UNITY_EDITOR

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
                
                Debug.Log("???");
            }

            return storeContainer;
        }
        
        private static string GetEditorTimeCacheDirectory()
        {
            return GetDirectoryPathFromAsset(GetEditorTimeStorageContainer());
        }
#endif

        private static string GetCacheLocation()
        {
#if UNITY_EDITOR
            return GetEditorTimeCacheDirectory();
#else
            return GetRuntimeCacheDirectory();
#endif
        }

        public static DataStoreContainer GetStoreContainer()
        {
#if UNITY_EDITOR
            return GetEditorTimeStorageContainer();
#else
            return GetRuntimeStoreContainer();
#endif
        }

        public static void SetupOrSaveCache()
        {
            string cachePath = GetCacheLocation();
        }

        private static List<FieldAttributeStore> fieldStores;
        private static List<MethodAttributeStore> methodStores;

        public static void EditorTimeCache<CachingItem, Attr>(
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
        }

        public static Dictionary<(Type, Type), T[]> GetCachedData<T>()
        {
            return null;
        }
    }
}
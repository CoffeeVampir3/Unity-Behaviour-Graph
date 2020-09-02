using System;
using UnityEngine;

namespace BehaviourGraph
{
    public static partial class AttributeCacheRetainer
    {
#if !UNITY_EDITOR
        private static DataStoreContainer storeContainer;
        private static string cachedDirectory = "";
        private static bool initialized = false;

        private static DataStoreContainer GetRuntimeStoreContainer()
        {
            if (initialized)
            {
                return storeContainer;
            }
            
            storeContainer = GameObject.FindObjectOfType<DataStoreContainer>();
            if(storeContainer == null)
                throw new Exception("Unity Behaviour Graph failed to find an attribute cache directory to build runtime values.");

            initialized = true;
            return storeContainer;
        }
        
        private static string GetRuntimeCacheDirectory() {
            if (initialized)
            {
                return cachedDirectory;
            }

            cachedDirectory = GetDirectoryPathFromAsset(GetRuntimeStoreContainer());
            return cachedDirectory;
        }
#endif
    }
}
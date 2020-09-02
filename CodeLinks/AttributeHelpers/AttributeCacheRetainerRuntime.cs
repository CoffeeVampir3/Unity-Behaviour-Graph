#if !UNITY_EDITOR
using System;
using UnityEngine;

namespace BehaviourGraph
{
    public static partial class AttributeCacheRetainer
    {
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
    }
}
#endif
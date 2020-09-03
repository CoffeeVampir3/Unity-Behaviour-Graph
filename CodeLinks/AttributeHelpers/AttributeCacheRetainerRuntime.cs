#if UNITY_EDITOR
using System;
using UnityEngine;

namespace BehaviourGraph
{
    public static partial class AttributeCacheRetainer
    {
        private static DataStoreContainer rtStoreContainer;
        private static bool initialized = false;

        private static DataStoreContainer GetRuntimeStoreContainer()
        {
            if (initialized)
            {
                return rtStoreContainer;
            }
            
            rtStoreContainer = GameObject.FindObjectOfType<DataStoreContainer>();
            if(rtStoreContainer == null)
                throw new Exception("Unity Behaviour Graph failed to find an attribute cache directory to build runtime values.");

            initialized = true;
            return rtStoreContainer;
        }
    }
}
#endif
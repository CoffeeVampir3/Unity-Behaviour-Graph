using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourGraph.CodeLinks
{
    internal partial class DataStoreContainer : SerializedScriptableObject
    {
        [SerializeField]
        private FieldAttributeStore[] fieldStores = null;
        [SerializeField]
        private MethodAttributeStore[] methodStores = null;

        internal List<StoreType> GetAttributeStoresOfType<StoreType, T>()
            where StoreType : AttributeStore
            where T : Attribute
        {
            Type storeType = typeof(StoreType);
            
            if (typeof(FieldAttributeStore).IsAssignableFrom(storeType))
            {
                return GetAttributeStoresOfType<FieldAttributeStore, T>(ref fieldStores) as List<StoreType>;
            } else if(typeof(MethodAttributeStore).IsAssignableFrom(storeType))
            {
                return GetAttributeStoresOfType<MethodAttributeStore, T>(ref methodStores) as List<StoreType>;
            }

            return null;
        }

        private List<StoreType> GetAttributeStoresOfType<StoreType, T>(ref StoreType[] allStores)
            where StoreType : AttributeStore
            where T : Attribute
        {
            List<StoreType> stores = new List<StoreType>();
            for (int i = 0; i < allStores?.Length; i++)
            {
                if (allStores[i].cachedAttributeType == typeof(T))
                {
                    stores.Add(allStores[i]);
                }
            }

            return stores;
        }

        internal List<FieldAttributeStore> GetFieldAttributeStoresOfType<T>()
            where T : Attribute
        {
            return GetAttributeStoresOfType<FieldAttributeStore, T>(ref fieldStores);
        }
        
        internal List<MethodAttributeStore> GetMethodAttributeStoresOfType<T>()
            where T : Attribute
        {
            return GetAttributeStoresOfType<MethodAttributeStore, T>(ref methodStores);
        }
    }
}
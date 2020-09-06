using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BehaviourGraph
{
    public partial class DataStoreContainer : SerializedScriptableObject
    {
        [SerializeField] 
        private FieldAttributeStore[] fieldStores;

        [SerializeField] 
        private MethodAttributeStore[] methodStores;

        public FieldAttributeStore[] GetFields()
        {
            return fieldStores;
        }

        public MethodAttributeStore[] GetMethods()
        {
            return methodStores;
        }
        
        private List<StoreType> GetAttributeStoresOfType<StoreType, T>(ref StoreType[] allStores )
            where StoreType : AttributeStore
        {
            List<StoreType> stores = new List<StoreType>();
            for (int i = 0; i < allStores.Length; i++)
            {
                if (allStores[i].cachedAttributeType == typeof(T))
                {
                    stores.Add(allStores[i]);
                }
            }

            return stores;
        }

        internal List<FieldAttributeStore> GetFieldAttributeStoresOfType<T>()
        {
            return GetAttributeStoresOfType<FieldAttributeStore, T>(ref fieldStores);
        }
        
        internal List<MethodAttributeStore> GetMethodAttributeStoresOfType<T>()
        {
            return GetAttributeStoresOfType<MethodAttributeStore, T>(ref methodStores);
        }
    }
}
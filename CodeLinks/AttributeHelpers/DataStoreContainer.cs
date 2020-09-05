using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph
{
    public class DataStoreContainer : SerializedScriptableObject
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
        
        private void StoreItem<T>(T item, ref T[] stores) where T : AttributeStore
        {
            if (stores == null)
            {
                stores = new T[1];
                stores[0] = item;
                return;
            }
            
            var temp = stores;
            stores = new T[temp.Length + 1];
            temp.CopyTo(stores, 0);
            stores[temp.Length] = item;
            
            EditorUtility.SetDirty(this);
        }
        
        public void StoreItem(AttributeStore item)
        {
            switch (item)
            {
                case FieldAttributeStore store:
                    StoreItem(store, ref fieldStores);
                    break;
                case MethodAttributeStore attributeStore:
                    StoreItem(attributeStore, ref methodStores);
                    break;
            }
        }
    }
}
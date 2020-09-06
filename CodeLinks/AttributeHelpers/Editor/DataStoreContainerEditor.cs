#if UNITY_EDITOR
using UnityEditor;

namespace BehaviourGraph
{
    public partial class DataStoreContainer
    {
                
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
#endif
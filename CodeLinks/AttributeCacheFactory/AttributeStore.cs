using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;

// Mono ~ Mono why would you serialize
// ~ Mono ~ Mono ~ 
//...
//...
//God is dead.

namespace BehaviourGraph
{
    public abstract class AttributeStore : SerializedScriptableObject
    {
        protected abstract void OnCreated(ref MemberInfo[] members);

        public abstract MemberInfo[] Retrieve();

        public static T Create<T>(ref MemberInfo[] members) where T : AttributeStore
        {
            T store = CreateInstance<T>();

            store.name = typeof(T).Name + " Type Store";
            store.OnCreated(ref members);
            AssetDatabase.CreateAsset(store, @"Assets\!Tests\" + store.name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(store));
            
            return store;
        }
        
    }
}
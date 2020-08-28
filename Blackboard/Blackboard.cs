using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [CreateAssetMenu]
    public class Blackboard : SerializedScriptableObject
    {
        [NonSerialized]
        protected GameObject owner;
        [SerializeField] 
        public List<BlackboardReference> blackboardReferences = new List<BlackboardReference>();
        
        #if UNITY_EDITOR
        [Button]
        internal void CreateReference()
        {
            var newRef = CreateInstance<BlackboardReference>();
            newRef.name = name + " reference " + blackboardReferences.Count;
            newRef.parentBlackboard = this;
            AssetDatabase.AddObjectToAsset(newRef, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newRef));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
            AssetDatabase.Refresh();
            blackboardReferences.Add(newRef);
        }
        #endif
        
        internal virtual void RuntimeInitialize(GameObject owner)
        {
            this.owner = owner;
            for (int i = 0; i < blackboardReferences.Count; i++)
            {
                if (blackboardReferences[i].referenceUsesGraphOwnerAsTarget)
                {
                    blackboardReferences[i].ReferencedObject = owner;
                }
                blackboardReferences[i].CacheRuntimeValues();
            }
        }

        internal virtual void RemoveReference(BlackboardReference reference)
        {
            if (blackboardReferences.Contains(reference))
            {
                blackboardReferences.Remove(reference);
            }
        }

        private ValueDropdownList<BlackboardReference> selectableExternalReferences;
        internal virtual ValueDropdownList<BlackboardReference> GetBlackboardReferences()
        {
            ValueDropdownList<BlackboardReference> allReferences = 
                GetListOfReferencesFor(blackboardReferences, ref selectableExternalReferences);

            return allReferences;
        }

        protected ValueDropdownList<BlackboardReference> GetListOfReferencesFor(
            List<BlackboardReference> references, ref ValueDropdownList<BlackboardReference> cachedListOfReferences)
        {
            if (cachedListOfReferences != null &&
                cachedListOfReferences.Count == references.Count)
            {
                return cachedListOfReferences;
            }
            
            cachedListOfReferences = new ValueDropdownList<BlackboardReference>();

            foreach (var bbRef in references)
            {
                if (bbRef.TryGetListName(out var listName))
                {
                    cachedListOfReferences.Add(name + "/" + listName, bbRef);
                }
            }
            cachedListOfReferences.Sort(
                (val1, 
                val2) => 
                String.Compare(val1.Text, val2.Text, StringComparison.Ordinal));
            
            return cachedListOfReferences;
        }
    }
}
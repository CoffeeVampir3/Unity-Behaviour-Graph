using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [CreateAssetMenu]
    public class SharedBlackboard : SerializedScriptableObject
    {
        [SerializeField] 
        public List<BlackboardReference> externalReferences = new List<BlackboardReference>();
        
        [Button]
        internal void CreateExternalReference()
        {
            var newRef = CreateInstance<BlackboardReference>();
            newRef.name = "External Reference " + externalReferences.Count;
            newRef.parentBlackboard = this;
            AssetDatabase.AddObjectToAsset(newRef, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newRef));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
            AssetDatabase.Refresh();
            externalReferences.Add(newRef);
        }
        
        internal virtual void RuntimeInitialize(GameObject owner)
        {
            for (int i = 0; i < externalReferences.Count; i++)
            {
                externalReferences[i].CacheRuntimeValues();
            }
        }

        internal virtual void RemoveReference(BlackboardReference reference)
        {
            if (externalReferences.Contains(reference))
            {
                externalReferences.Remove(reference);
            }
        }

        private ValueDropdownList<BlackboardReference> selectableExternalReferences;
        internal virtual ValueDropdownList<BlackboardReference> GetBlackboardReferences()
        {
            ValueDropdownList<BlackboardReference> allReferences = 
                GetListOfReferencesFor(externalReferences, ref selectableExternalReferences);

            return allReferences;
        }
        
        protected ValueDropdownList<BlackboardReference> GetListOfReferencesFor(
            List<BlackboardReference> references, ref ValueDropdownList<BlackboardReference> cachedListOfReferences ) 
        {
            
            if (cachedListOfReferences != null &&
                cachedListOfReferences.Count == references.Count)
            {
                return cachedListOfReferences;
            }
            
            cachedListOfReferences = new ValueDropdownList<BlackboardReference>();

            foreach (var bbRef in references)
            {
                string formattedInfoString = "";
                if (bbRef.TryGetDisplayName(out formattedInfoString))
                {
                    cachedListOfReferences.Add(formattedInfoString, bbRef);
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
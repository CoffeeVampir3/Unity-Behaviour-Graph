﻿using System;
using System.Collections.Generic;
using Coffee.Behaviour.Nodes;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    public class LocalBlackboard : SharedBlackboard
    {
        [NonSerialized]
        protected GameObject owner;
        [SerializeField] 
        public List<BlackboardReference> selfReferences = new List<BlackboardReference>();
        
        [Button]
        public void CreateSelfReference()
        {
            var newRef = CreateInstance<BlackboardReference>();
            newRef.name = "Self Reference " + selfReferences.Count;
            newRef.parentBlackboard = this;
            AssetDatabase.AddObjectToAsset(newRef, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newRef));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
            AssetDatabase.Refresh();
            selfReferences.Add(newRef);
        }
        
        public override void RemoveReference(BlackboardReference reference)
        {
            base.RemoveReference(reference);
            if (selfReferences.Contains(reference))
            {
                selfReferences.Remove(reference);
            }
        }

        public override void RuntimeInitialize(GameObject owner)
        {
            base.RuntimeInitialize(owner);
            this.owner = owner;
            for (int i = 0; i < selfReferences.Count; i++)
            {
                selfReferences[i].ReferencedObject = owner;
                selfReferences[i].CacheRuntimeValues();
            }
        }
        
        public override ValueDropdownList<BlackboardReference> GetBlackboardReferences()
        {
            ValueDropdownList<BlackboardReference> allReferences = base.GetBlackboardReferences();
            var temp = GetListOfReferencesFor(selfReferences, ref selectableSelfReferences);
            if(temp != null)
                allReferences.AddRange(temp);
            
            return allReferences;
        }

        private ValueDropdownList<BlackboardReference> selectableSelfReferences;
    }
}
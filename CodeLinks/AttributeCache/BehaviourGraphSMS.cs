using System.Reflection;
using BehaviourGraph.Attributes;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    //TODO:: Refactor this garbage
    public class BehaviourGraphSMS : SerializedMemberStore
    {
        #if UNITY_EDITOR
        [InitializeOnLoadMethod]
        public static void SetupSingleton()
        {
            var singleton = GetInstance();
            if (singleton == null)
            {
                var inst = ScriptableObject.CreateInstance<BehaviourGraphSMS>();
                inst.name = "bgsms";
                instance = inst.Create(inst);
            }
            else
            {
                if(singleton is BehaviourGraphSMS bgsms)
                    bgsms.ReflectAndStore();
            }
        }

        private void ReflectAndStore()
        {
            ClearCacheData();
            Cache<MethodInfo, Condition>();
            Cache<FieldInfo, Condition>();
            Cache<MethodInfo, Service>();
            EditorUtility.SetDirty(this);
        }

        private SerializedMemberStore Create(BehaviourGraphSMS inst)
        {
            AssetDatabase.CreateAsset(inst, @"Assets/Resources/" + inst.name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(inst));
            AssetDatabase.Refresh();

            inst.ReflectAndStore();
            instance = inst;
            return instance;
        }
        
        #endif
    }
}
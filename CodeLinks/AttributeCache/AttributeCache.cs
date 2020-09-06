using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    public class AttributeCache : SerializedScriptableObject
    {
        [NonSerialized, OdinSerialize]
        private FieldInfoStore fieldStore = new FieldInfoStore();
        [NonSerialized, OdinSerialize]
        private MethodInfoStore methodStore = new MethodInfoStore();

        private void SetDirtyWrapper()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public void Cache<T>(List<FieldInfo> fields)
            where T : Attribute
        {
            fieldStore.Store<T>(fields);
            SetDirtyWrapper();
        }
        public void Cache<T>(List<MethodInfo> methods)
            where T : Attribute
        {
            methodStore.Store<T>(methods);
            SetDirtyWrapper();
        }

        public bool Get<T>(out List<FieldInfo> fields)
        {
            fields = fieldStore.GetRuntimeFieldData<T>();
            return fields == null;
        }
        
        public bool Get<T>(out List<MethodInfo> methods)
        {
            methods = methodStore.GetRuntimeMethodData<T>();
            return methods == null;
        }
        
    }
}
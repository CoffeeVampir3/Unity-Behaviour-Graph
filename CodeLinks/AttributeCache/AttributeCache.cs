using System;
using System.Collections.Generic;
using System.Linq;
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
        [NonSerialized, OdinSerialize]
        private MemberLookupStore memberLookupStore = new MemberLookupStore();

        //TODO:: TEMP CODE
        private void SetDirtyWrapper()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        
        public void Cache<ItemType, Attr>()
            where ItemType : MemberInfo
            where Attr : Attribute
        {
            Type iType = typeof(ItemType);

            if (typeof(FieldInfo).IsAssignableFrom(iType))
            {
                Cache<Attr>(
                    TypeCache.
                        GetFieldsWithAttribute<Attr>().
                        ToList()
                    );
            }
            else if(typeof(MethodInfo).IsAssignableFrom(iType))
            {
                Cache<Attr>(
                    TypeCache.
                        GetMethodsWithAttribute<Attr>().
                        ToList()
                );
            }
        }

        public void Cache<T>(List<FieldInfo> fields)
            where T : Attribute
        {
            fieldStore.Store<T>(fields, memberLookupStore);
            SetDirtyWrapper();
        }
        public void Cache<T>(List<MethodInfo> methods)
            where T : Attribute
        {
            methodStore.Store<T>(methods, memberLookupStore);
            SetDirtyWrapper();
        }

        public static string MemberToString(MemberInfo member)
        {
            return MemberLookupStore.MemberToString(member);
        }

        public MemberInfo LookupByString(string value)
        {
            return memberLookupStore.TryGetValue(value, out var member) 
                ? member : null;
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
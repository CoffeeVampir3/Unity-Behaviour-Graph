using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    public class SerializedMemberStore : SerializedScriptableObject
    {
        [NonSerialized, OdinSerialize]
        private Dictionary<string, ISerializedMemberInfo> memberLookup = 
            new Dictionary<string, ISerializedMemberInfo>();

        [NonSerialized, OdinSerialize]
        private Dictionary<Type, List<ISerializedMemberInfo>> fieldsByAttribute = 
            new Dictionary<Type, List<ISerializedMemberInfo>>();
        
        [NonSerialized, OdinSerialize]
        private Dictionary<Type, List<ISerializedMemberInfo>> methodsByAttribute = 
            new Dictionary<Type, List<ISerializedMemberInfo>>();
        
        [NonSerialized, OdinSerialize]
        private Dictionary<Type, List<ISerializedMemberInfo>> membersByAttribute = 
            new Dictionary<Type, List<ISerializedMemberInfo>>();
        
        #region Accessors
        
        public static bool TryGetMembersByAttribute<Attr>(out List<MemberInfo> memberInfo)
            where Attr : Attribute
        {
            if (GetInstance().membersByAttribute.TryGetValue(typeof(Attr), out var serialList))
            {
                List<MemberInfo> members = new List<MemberInfo>();
                foreach (var item in serialList)
                {
                    members.Add(item.Get());
                }
                
                memberInfo = members;
                return true;
            }
            
            memberInfo = null;
            return false;
        }

        public static MemberInfo LookupByString(string value)
        {
            return GetInstance().memberLookup.TryGetValue(value, out var member) 
                ? member.Get() : null;
        }
        
        #endregion
        
        #region Helpers
        
        public static string MemberToString(MemberInfo member)
        {
            //Meaningless assertion but whatever.
            Debug.Assert(member.ReflectedType != null, "member.ReflectedType != null");
            return member.ReflectedType.Name + "/" + member.Name;
        }
        
        #endregion

        #region Singleton GetInstance impl
        
        private static SerializedMemberStore instance;
        private void Reset()
        {
            instance = this;
        }

        private static SerializedMemberStore GetInstance()
        {
            if (instance != null)
            {
                return instance;
            }

            var stores = Resources.FindObjectsOfTypeAll<SerializedMemberStore>();
            if (stores.Any())
            {
                instance = stores[0];
            }
            else
            {
                Debug.LogError("No serialized member store found.");
                //Create instance here?
            }
            return instance;
        }
        
        #endregion

        #region Cache Impl

        public void Cache<ItemType, Attr>()
            where ItemType : MemberInfo
            where Attr : Attribute
        {
            Type iType = typeof(ItemType);
            
            Debug.Assert(typeof(FieldInfo).IsAssignableFrom(iType) 
                         || typeof(MethodInfo).IsAssignableFrom(iType));

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
        
        private void MergeMembersByAttribute<Attr>(List<ISerializedMemberInfo> info)
            where Attr : Attribute
        {
            Type attribType = typeof(Attr);
            
            if (membersByAttribute.TryGetValue(attribType, out var existing))
            {
                existing.AddRange(info);
                return;
            }

            membersByAttribute.Add(attribType, info);
        }
        
        private void Cache<Attr>(List<FieldInfo> fieldList)
            where Attr : Attribute
        {
            var infoList = new List<ISerializedMemberInfo>();
            Type attribType = typeof(Attr);
            
            foreach (var field in fieldList)
            {
                var serializedItem = new SerializedFieldInfo(field);
                infoList.Add(serializedItem);
                
                memberLookup.Add(MemberToString(field), serializedItem);
            }
            
            fieldsByAttribute.Remove(attribType);
            fieldsByAttribute.Add(attribType, infoList);
            MergeMembersByAttribute<Attr>(infoList);
            
            EditorUtility.SetDirty(this);
        }
        
        private void Cache<Attr>(List<MethodInfo> methodList)
            where Attr : Attribute
        {
            var infoList = new List<ISerializedMemberInfo>();
            Type attribType = typeof(Attr);
            
            foreach (var method in methodList)
            {
                var serializedItem = new SerializedMethodInfo(method);
                infoList.Add(serializedItem);
                
                memberLookup.Add(MemberToString(method), serializedItem);
            }
            
            methodsByAttribute.Remove(attribType);
            methodsByAttribute.Add(attribType, infoList);
            MergeMembersByAttribute<Attr>(infoList);
            EditorUtility.SetDirty(this);
        }
        
        #endregion
    }
}
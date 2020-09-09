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
    public abstract class SerializedMemberStore : SerializedScriptableObject
    {
        [NonSerialized, OdinSerialize]
        protected Dictionary<string, ISerializedMemberInfo> memberLookup = 
            new Dictionary<string, ISerializedMemberInfo>();

        [NonSerialized, OdinSerialize]
        protected Dictionary<Type, List<ISerializedMemberInfo>> fieldsByAttribute = 
            new Dictionary<Type, List<ISerializedMemberInfo>>();
        
        [NonSerialized, OdinSerialize]
        protected Dictionary<Type, List<ISerializedMemberInfo>> methodsByAttribute = 
            new Dictionary<Type, List<ISerializedMemberInfo>>();
        
        [NonSerialized, OdinSerialize]
        protected Dictionary<Type, List<ISerializedMemberInfo>> membersByAttribute = 
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
            Debug.Log(value);
            var k = GetInstance();
            if (GetInstance() == null)
            {
                Debug.LogError("Lookup instance is null.");
            }
            
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

        protected void ClearCacheData()
        {
            memberLookup.Clear();
            fieldsByAttribute.Clear();
            methodsByAttribute.Clear();
            membersByAttribute.Clear();
        }
        
        #endregion

        #region Singleton GetInstance impl
        
        protected static SerializedMemberStore instance;

        protected virtual void Reset()
        {
            instance = this;
        }
        
        public static SerializedMemberStore GetInstance()
        {
            if (instance != null)
            {
                return instance;
            }
            
            instance = Resources.Load<SerializedMemberStore>("bgsms");
            
            if (!instance)
            {
                Debug.LogError("Store is not fetchable during runtime dtus.");
                return null;
            }

            return instance;
        }

        #endregion

        #if UNITY_EDITOR
        
        #region Cache Impl

        protected void Cache<ItemType, Attr>()
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
        
        protected void MergeMembersByAttribute<Attr>(List<ISerializedMemberInfo> info)
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
        
        protected void Cache<Attr>(List<FieldInfo> fieldList)
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
        
        protected void Cache<Attr>(List<MethodInfo> methodList)
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
        
        #endif
    }
}
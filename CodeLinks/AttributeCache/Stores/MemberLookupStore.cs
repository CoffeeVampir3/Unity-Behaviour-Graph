using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.Serialization;
using UnityEngine;

//TODO::
//Optimizable by using array indices (in the store) and storing an index lookup
//in the array, instead of a double-dictionary lookup

namespace BehaviourGraph.CodeLinks.AttributeCache
{
    [Serializable]
    internal class MemberLookupStore
    {
        [OdinSerialize]
        private Dictionary<string, MemberStoredHash> memberLookup = 
            new Dictionary<string, MemberStoredHash>();

        [Serializable]
        private struct MemberStoredHash
        {
            public IMemberStore store;
            public int itemHash;

            public MemberStoredHash(IMemberStore store, int itemHash)
            {
                this.store = store;
                this.itemHash = itemHash;
            }
            
            public MemberInfo MemberFromInfo => store.GetMemberByHash(itemHash);
        }

        public static string MemberToString(MemberInfo member)
        {
            return member.ReflectedType.Name + "/" + member.Name;
        }

        public void Add(MemberInfo member, IMemberStore store)
        {
            Debug.Assert(member != null, "member != null");
            MemberStoredHash stored = 
                new MemberStoredHash(store, member.GetHashCode());
            
            memberLookup.Add(MemberToString(member), stored);
        }

        public bool TryGetValue(string value, out MemberInfo member)
        {
            if (!memberLookup.TryGetValue(value, out var storeWithHash))
            {
                member = null;
                return false;
            }

            member = storeWithHash.MemberFromInfo;
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [Serializable]
    public class BlackboardDefaultDataContainer : IBlackboard
    {
        [OdinSerialize] private object[] blackboard;
        [OdinSerialize] private Type[] blackboardTypes;
        [OdinSerialize, HideInInspector] private int compileTimeAllocationSize = -1;

        [SerializeField]
        private int currentIndex = -1;
        [SerializeField]
        private Stack<int> hangingIndices = new Stack<int>();

        public void Initialize(int allocationSize)
        {
            if (compileTimeAllocationSize == allocationSize)
                return;
            
            compileTimeAllocationSize = allocationSize;
            blackboard = new object[allocationSize];
            blackboardTypes = new Type[allocationSize];
        }
        
        private void ResizeBlackboard()
        {
            if (currentIndex >= blackboard.Length - 2)
            {
                object[] oldBlackboardData = blackboard;
                blackboard = new object[blackboard.Length * 2];
                oldBlackboardData.CopyTo(blackboard, 0);

                Type[] oldTypes = blackboardTypes;
                blackboardTypes = new Type[blackboardTypes.Length * 2];
                oldTypes.CopyTo(blackboardTypes, 0);
            }
        }

        public T GetItem<T>(int index)
        {
            return (T) blackboard[index];
        }

        public object GetItem(int index)
        {
            return blackboard[index];
        }

        public Type GetType(int index)
        {
            return blackboardTypes[index];
        }

        public void ReleaseId(int id)
        {
            blackboard[id] = null;
            hangingIndices.Push(id);
        }

        private int GetFreeID()
        {
            if (hangingIndices.Any())
            {
                return hangingIndices.Pop();
            }

            return ++currentIndex;
        }

        public int SetItem<T>(T item)
        {
            ResizeBlackboard();
            int id = GetFreeID();
            blackboard[id] = item;
            blackboardTypes[id] = typeof(T);
            return id;
        }
    }
}
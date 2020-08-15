using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviourGraph.Blackboard
{
    [Serializable]
    public class BlackboardBase : IBlackboard
    {
        private object[] blackboard = new object[100];
        [SerializeField]
        private int currentIndex = -1;
        [SerializeField]
        private Stack<int> hangingIndices = new Stack<int>();
        
        private void ResizeBlackboard()
        {
            if (currentIndex >= blackboard.Length - 2)
            {
                blackboard = new object[blackboard.Length * 2];
            }
        }

        public T GetItem<T>(int index)
        {
            return (T) blackboard[index];
        }

        public void ReleaseId(int id)
        {
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
            return id;
        }
    }
}
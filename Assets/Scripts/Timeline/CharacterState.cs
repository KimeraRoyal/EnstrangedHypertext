using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EHT
{
    [Serializable]
    public class CharacterState
    {
        private HashSet<string> memoryIds = new();
        [SerializeField] private List<string> memoryIdsForDisplay = new();

        public void AddMemory(string id)
        {
            if(memoryIds.Add(id)) { memoryIdsForDisplay.Add(id); }
        }

        public void RemoveMemory(string id)
            => memoryIds.Remove(id);
        
        public bool HasMemory(string id)
            => memoryIds.Contains(id);

        public void Reset()
        {
            memoryIds.Clear();
            memoryIdsForDisplay.Clear();
        }
        
        public void Copy(CharacterState from)
        {
            memoryIds = new HashSet<string>(from.memoryIds);
            memoryIdsForDisplay = new List<string>(memoryIds);
        }

        public void Inherit(CharacterState from)
        {
            memoryIds = memoryIds.Concat(from.memoryIds).ToHashSet();
            memoryIdsForDisplay = new List<string>(memoryIds);
        }
    }
}

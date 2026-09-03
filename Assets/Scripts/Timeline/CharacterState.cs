using System;
using System.Collections.Generic;

namespace EHT
{
    [Serializable]
    public class CharacterState
    {
        private HashSet<string> memoryIds = new();

        public void AddMemory(string id)
            => memoryIds.Add(id);

        public void RemoveMemory(string id)
            => memoryIds.Remove(id);
        
        public bool HasMemory(string id)
            => memoryIds.Contains(id);

        public void Copy(CharacterState from)
        {
            memoryIds = new HashSet<string>(from.memoryIds);
        }
    }
}

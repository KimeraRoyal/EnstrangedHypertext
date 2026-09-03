using System;
using System.Collections.Generic;
using UnityEngine;

namespace EHT
{
    [CreateAssetMenu(menuName = "EHT/Memory Bank", fileName = "Memory Bank")]
    public class MemoryBank : ScriptableObject
    {
        [SerializeField] private Memory[] memoryEntries;
        private readonly Dictionary<string, Memory> memories = new();

        private void CompileDictionary()
        {
            if(memoryEntries.Length == memories.Count) { return; }
            
            memories.Clear();
            foreach (var entry in memoryEntries)
            {
                if(!entry) { continue; }
                memories.Add(entry.ID, entry);
            }
        }

        public Memory GetMemory(string id)
        {
            CompileDictionary(); 
            return memories.GetValueOrDefault(id.ToLower());
        }
        
        private void OnValidate()
        {
            CompileDictionary();
        }
    }
}

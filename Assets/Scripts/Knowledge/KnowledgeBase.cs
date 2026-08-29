using System;
using System.Collections.Generic;
using UnityEngine;

namespace EHT.Knowledge
{
    [CreateAssetMenu(menuName = "EHT/Knowledge Base", fileName = "Knowledge Base")]
    public class KnowledgeBase : ScriptableObject
    {
        [Serializable]
        private struct KnowledgeItemEntry
        {
            public string id;
            public KnowledgeItem item;
        }

        [SerializeField] private KnowledgeItemEntry[] itemEntries;
        private readonly Dictionary<string, KnowledgeItem> items = new();

        private void CompileDictionary()
        {
            if(itemEntries.Length == items.Count) { return; }
            
            items.Clear();
            foreach (var entry in itemEntries)
            {
                items.Add(entry.id.ToLower(), entry.item);
            }
        }

        public KnowledgeItem GetItem(string id)
        {
            CompileDictionary(); 
            return items.GetValueOrDefault(id.ToLower());
        }
        
        private void OnValidate()
        {
            CompileDictionary();
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EHT.Knowledge
{
    [CreateAssetMenu(menuName = "EHT/Knowledge Base", fileName = "Knowledge Base")]
    public class KnowledgeBase : ScriptableObject
    {
        [SerializeField] private KnowledgeItem[] itemEntries;
        private readonly Dictionary<string, KnowledgeItem> items = new();

        private void CompileDictionary()
        {
            if(itemEntries.Length == items.Count) { return; }
            
            items.Clear();
            foreach (var entry in itemEntries)
            {
                if(!entry) { continue; }
                items.Add(entry.ID, entry);
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
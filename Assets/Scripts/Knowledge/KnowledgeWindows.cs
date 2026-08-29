using System.Collections.Generic;
using UnityEngine;

namespace EHT.Knowledge
{
    public class KnowledgeWindows : MonoBehaviour
    {
        [SerializeField] private KnowledgeWindow windowPrefab;

        private Dictionary<string, KnowledgeWindow> windows = new();
        
        public KnowledgeWindow OpenWindow(string id, KnowledgeItem item)
        {
            id = id.ToLower();
            if (windows.TryGetValue(id, out var window))
            {
                return window;
            }

            window = Instantiate(windowPrefab, transform);
            window.KnowledgeItem = item;
            
            windows.Add(id, window);
            return window;
        }

        public bool IsWindowOpen(string id)
            => windows.ContainsKey(id.ToLower());
    }
}
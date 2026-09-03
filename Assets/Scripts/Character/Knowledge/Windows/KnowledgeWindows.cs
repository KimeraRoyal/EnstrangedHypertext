using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace EHT.Knowledge.Windows
{
    public class KnowledgeWindows : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int maxWindows = 6;
        [SerializeField] private KnowledgeWindow windowPrefab;

        private readonly List<KnowledgeWindow> windows = new();

        public int MaxWindows => maxWindows;

        public IReadOnlyList<KnowledgeWindow> Windows => windows;

        public UnityEvent OnWindowStateChanged;
        
        public KnowledgeWindow OpenWindow(string id, KnowledgeItem item)
        {
            id = id.ToLower();
            
            var window = FindWindow(id);
            if (window)
            {
                window.IsShown = true;
                return window;
            }

            if (windows.Count >= maxWindows)
            {
                CloseWindow(windows[0]);
            }

            window = Instantiate(windowPrefab, transform);

            window.ID = id;
            window.KnowledgeItem = item;
            windows.Add(window);
            
            OnWindowStateChanged?.Invoke();
            
            return window;
        }

        public void CloseWindow(string id)
            => CloseWindow(FindWindow(id));

        public void CloseWindow(KnowledgeWindow window)
        {
            windows.Remove(window);
            OnWindowStateChanged?.Invoke();
            
            Destroy(window.gameObject);
        }
        
        public KnowledgeWindow FindWindow(string id)
            => windows.FirstOrDefault(window => window.ID == id);

        public bool IsWindowOpen(string id)
            => FindWindow(id);
    }
}
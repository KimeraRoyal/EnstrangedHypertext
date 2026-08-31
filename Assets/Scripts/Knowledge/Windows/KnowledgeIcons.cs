using EHT.Knowledge.Windows;
using UnityEngine;

namespace EHT
{
    public class KnowledgeIcons : MonoBehaviour
    {
        private KnowledgeWindows windows;
        
        [SerializeField] private KnowledgeIcon iconPrefab;
        private KnowledgeIcon[] icons;

        private void Awake()
        {
            windows = GetComponentInParent<KnowledgeWindows>();
        }

        private void Start()
        {
            icons = new KnowledgeIcon[windows.MaxWindows];
            for (var i = 0; i < windows.MaxWindows; i++)
            {
                icons[i] = Instantiate(iconPrefab, transform);
                icons[i].TargetIndex = i;
            }
        }
    }
}

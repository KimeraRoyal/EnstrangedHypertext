using EHT.Knowledge;
using UnityEngine;

namespace EHT.Timeline
{
    [CreateAssetMenu(menuName = "EHT/Character", fileName = "Character")]
    public class Character : ScriptableObject
    {
        [SerializeField] private string id;
        
        [SerializeField] private Color color = Color.white;
        [SerializeField] private Color backgroundColor = Color.black;
        
        [SerializeField] private KnowledgeBase knowledgeBase;
        [SerializeField] private MemoryBank memoryBank;

        public string ID => id;

        public Color Color => color;
        public Color BackgroundColor => backgroundColor;

        public KnowledgeBase KnowledgeBase => knowledgeBase;

        private void Awake()
        {
            GenerateID();
        }

        private void OnValidate()
        {
            GenerateID();
            id = id.ToLower();
        }

        private void GenerateID()
        {
            if (!string.IsNullOrEmpty(id)) { return; }
            id = name.ToLower().Replace(' ', '-');
        }
    }
}

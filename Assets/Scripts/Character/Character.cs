using EHT.Knowledge;
using UnityEngine;

namespace EHT.Timeline
{
    [CreateAssetMenu(menuName = "EHT/Character", fileName = "Character")]
    public class Character : ScriptableObject
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private Color backgroundColor = Color.black;
        
        [SerializeField] private KnowledgeBase knowledgeBase;
        [SerializeField] private MemoryBank memoryBank;

        public Color Color => color;
        public Color BackgroundColor => backgroundColor;

        public KnowledgeBase KnowledgeBase => knowledgeBase;
    }
}

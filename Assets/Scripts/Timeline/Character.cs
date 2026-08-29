using EHT.Knowledge;
using UnityEngine;

namespace EHT.Timeline
{
    [CreateAssetMenu(menuName = "EHT/Character", fileName = "Character")]
    public class Character : ScriptableObject
    {
        [SerializeField] private Color color = Color.white;

        [SerializeField] private KnowledgeBase knowledgeBase;

        public Color Color => color;

        public KnowledgeBase KnowledgeBase => knowledgeBase;
    }
}

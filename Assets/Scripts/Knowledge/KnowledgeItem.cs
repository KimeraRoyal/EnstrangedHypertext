using UnityEngine;

namespace EHT.Knowledge
{
    [CreateAssetMenu(menuName = "EHT/Knowledge Item", fileName = "Knowledge Item")]
    public class KnowledgeItem : ScriptableObject
    {
        [SerializeField] [TextArea(3, 5)] private string description;
    }
}

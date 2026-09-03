using UnityEngine;

namespace EHT.Knowledge
{
    [CreateAssetMenu(menuName = "EHT/Knowledge Item", fileName = "Knowledge Item")]
    public class KnowledgeItem : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] [TextArea(3, 5)] private string description;

        public string ID => id;
        public string Description => description;

        private void Awake()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name.ToLower().Replace(' ', '-');
            }
        }

        private void OnValidate()
        {
            id = id.ToLower();
        }
    }
}

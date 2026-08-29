using EHT.Knowledge;
using TMPro;
using UnityEngine;

namespace EHT
{
    public class KnowledgeWindow : MonoBehaviour
    {
        [SerializeField] private KnowledgeItem knowledgeItem;
        
        public KnowledgeItem KnowledgeItem
        {
            get => knowledgeItem;
            set
            {
                knowledgeItem = value;

                titlebarLabel.text = knowledgeItem.name;
                content.text = knowledgeItem.Description;
            }
        }

        [SerializeField] private TMP_Text titlebarLabel;
        [SerializeField] private TMP_Text content;
    }
}

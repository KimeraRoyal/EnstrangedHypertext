using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace EHT.Knowledge.Windows
{
    public class KnowledgeWindow : MonoBehaviour
    {
        private KnowledgeWindows windows;

        private CanvasGroup canvasGroup;
        
        [SerializeField] private string id;
        [SerializeField] private KnowledgeItem knowledgeItem;

        private bool isShown = true;

        [SerializeField] private TMP_Text titlebarLabel;
        [SerializeField] private TMP_Text content;

        public string ID
        {
            get => id;
            set => id = value;
        }

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
        
        public bool IsShown
        {
            get => isShown;
            set
            {
                if(isShown == value) { return; }
                isShown = value;

                canvasGroup.alpha = isShown ? 1.0f : 0.0f;
                canvasGroup.interactable = isShown;
                canvasGroup.blocksRaycasts = isShown;
                
                OnShown?.Invoke(isShown);
            } 
        }

        public UnityEvent<bool> OnShown;

        private void Awake()
        {
            windows = GetComponentInParent<KnowledgeWindows>();

            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Close()
            => windows.CloseWindow(this);
    }
}

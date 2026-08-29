using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace EHT.Knowledge
{
    [RequireComponent(typeof(TMP_Text))]
    public class KnowledgeLinks : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private KnowledgeBase knowledgeBase;
        private KnowledgeWindows knowledgeWindows;
        
        private TMP_Text text;

        [SerializeField] private InputActionReference mousePosition;

        private void Awake()
        {
            knowledgeWindows = FindAnyObjectByType<KnowledgeWindows>();
            
            text = GetComponent<TMP_Text>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, mousePosition.action.ReadValue<Vector2>(), null);
            
            if(linkIndex < 0) { return; }
            
            var id = text.textInfo.linkInfo[linkIndex].GetLinkID();
            var item = knowledgeBase.GetItem(id);
            if(!item) { return; }

            knowledgeWindows.OpenWindow(id, item);
        }
    }
}
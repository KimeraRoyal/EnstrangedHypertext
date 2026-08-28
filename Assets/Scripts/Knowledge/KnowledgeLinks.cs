using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace EHT.Knowledge
{
    [RequireComponent(typeof(TMP_Text))]
    public class KnowledgeLinks : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text text;

        [SerializeField] private InputActionReference mousePosition;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, mousePosition.action.ReadValue<Vector2>(), null);
            
            if(linkIndex < 0) { return; }
            
            var id = text.textInfo.linkInfo[linkIndex].GetLinkID();
            
            Debug.Log(id);
            
            // Behaviour
        }
    }
}
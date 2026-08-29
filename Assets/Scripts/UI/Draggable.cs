using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace EHT
{
    [RequireComponent(typeof(RectTransform))]
    public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool dragging;

        [SerializeField] private RectTransform dragTarget;

        [SerializeField] private InputActionReference pointer;
        private Vector2 lastPointerPosition;

        private void Start()
        {
            if (dragTarget == null) { dragTarget = GetComponent<RectTransform>(); }
        }

        private void OnEnable()
        {
            pointer.action.performed += OnPointerMoved;
        }

        private void OnDisable()
        {
            pointer.action.performed -= OnPointerMoved;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            dragging = true;
            lastPointerPosition = pointer.action.ReadValue<Vector2>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            dragging = false;
        }

        private void OnPointerMoved(InputAction.CallbackContext context)
        {
            if(!dragging) { return; }
            
            var position = context.ReadValue<Vector2>();
            dragTarget.anchoredPosition += position - lastPointerPosition;
            lastPointerPosition = position;
        }
    }
}

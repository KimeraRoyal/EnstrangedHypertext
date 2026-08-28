using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace EHT.Narrative.Reader.Wait
{
    public class InputIndicator : MonoBehaviour
    {
        private Image image;

        [SerializeField] private InputActionReference progressAction;

        public bool Active
        {
            get => image.enabled;
            private set
            {
                if(image.enabled == value) { return; }
                image.enabled = value;
                if(value) { OnActivated?.Invoke(); }
                else { OnDeactivated?.Invoke(); }
            }
        }

        public UnityEvent OnActivated;
        public UnityEvent OnDeactivated;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            progressAction.action.started += InputPressed;
        }

        private void OnDisable()
        {
            progressAction.action.started += InputPressed;
        }

        private void Start()
        {
            Active = false;
        }

        public void Activate()
        {
            Active = true;
        }

        private void InputPressed(InputAction.CallbackContext obj)
        {
            Active = false;
        }
    }
}

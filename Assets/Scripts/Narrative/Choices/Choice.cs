using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EHT.Narrative.Choices
{
    public class Choice : MonoBehaviour
    {
        private Button button;
        private TMP_Text label;

        public int Index { get; set; }
        public string Label { get => label.text; set => label.text = value; }

        public UnityEvent<int> OnChoiceSelected;

        private void Awake()
        {
            button = GetComponentInChildren<Button>();
            label = GetComponentInChildren<TMP_Text>();

            button.onClick.AddListener(Select);
        }

        private void Select()
        {
            OnChoiceSelected?.Invoke(Index);
        }
    }
}
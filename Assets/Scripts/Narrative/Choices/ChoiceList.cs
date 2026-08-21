using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EHT.Narrative.Choices
{
    public class ChoiceList : MonoBehaviour
    {
        [SerializeField] private Choice choicePrefab;
        private List<Choice> choices = new();
        private int selectedChoice = -1;

        public UnityEvent<int> OnChoiceSelected;

        public void AddChoice(string choiceLabel)
        {
            selectedChoice = -1;

            var choice = Instantiate(choicePrefab, transform);
            choice.Index = choices.Count;
            choice.Label = choiceLabel;
            choice.OnChoiceSelected.AddListener(SelectChoice);
            choices.Add(choice);
        }

        public void ClearChoices()
        {
            if(choices.Count < 1) { return; }
            foreach(var choice in choices)
            {
                Destroy(choice.gameObject);
            }
            choices.Clear();
        }

        private void SelectChoice(int index)
        {
            if(selectedChoice >= 0) { return; }
            selectedChoice = index;
            OnChoiceSelected?.Invoke(selectedChoice);
        }
    }
}
using System;
using System.Collections;
using EHT.Narrative.Choices;
using Ink.Runtime;
using UnityEngine;

namespace EHT.Narrative
{
    public class NarrativeSystem : MonoBehaviour
    {
        public static event Action<Story> OnCreateStory;

        [SerializeField] private Reader.Reader currentReader;
        [SerializeField] private ChoiceList choices;

        private Story story;
        private bool running;

        public bool Running => running;

        private void Awake()
        {
            choices.OnChoiceSelected.AddListener(SelectChoice);
            
            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void Create(TextAsset storyAsset)
        {
            story = new Story(storyAsset.text);
            OnCreateStory?.Invoke(story);
        }

        public void Begin()
        {
            if(running) { return; }

            transform.GetChild(0).gameObject.SetActive(true);
            running = true;
            
            Progress();
        }

        public object GetVariable(string variableName)
            => story.variablesState[variableName];

        public void SetVariable(string variableName, object value)
            => story.variablesState[variableName] = value;

        private void Progress()
        {
            StartCoroutine(ReadLines());
        }

        private IEnumerator ReadLines()
        {
            currentReader.ClearLines();
            while(story.canContinue)
            {
                var text = story.Continue();
                text = text.Trim();
                currentReader.DecodeLine(text);
            }
            currentReader.Process();

            if(currentReader.Busy) { yield return new WaitUntil(() => !currentReader.Busy); }

            PresentChoices();
        }

        private void PresentChoices()
        {
            if(story.currentChoices.Count < 1)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                running = false;
                
                return;
            }

            for(var i = 0; i < story.currentChoices.Count; i++)
            {
                choices.AddChoice(story.currentChoices[i].text);
            }
        }

        private void SelectChoice(int index)
        {
		    story.ChooseChoiceIndex (index);
            choices.ClearChoices();

            Progress();
        }
    }
}
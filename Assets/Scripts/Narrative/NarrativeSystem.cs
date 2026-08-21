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

        [SerializeField] private TextAsset storyAsset;
        private Story story;

        private void Awake()
        {
            choices.OnChoiceSelected.AddListener(SelectChoice);
        }

        private void Start()
        {
            Begin();
        }

        private void Begin()
        {
            story = new Story(storyAsset.text);
            OnCreateStory?.Invoke(story);
            Progress();
        }

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
                // Finished
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
using System;
using System.Collections;
using EHT.Narrative.Choices;
using EHT.Narrative.Reader.Wait;
using Ink.Runtime;
using UnityEngine;
using Choice = Ink.Runtime.Choice;

namespace EHT.Narrative
{
    public class NarrativeSystem : MonoBehaviour
    {
        public static event Action<Story> OnCreateStory;

        [SerializeField] private Reader.Reader currentReader;
        [SerializeField] private ChoiceList choices;

        private Story story;
        private bool running;

        public Story Story => story;
        public bool Running => running;

        public Action OnStoryCreated;
        public Action OnStoryStarted;
        public Action OnStoryFinished;

        private void Awake()
        {
            choices.OnChoiceSelected.AddListener(SelectChoice);
            
            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void Create(TextAsset storyAsset)
        {
            story = new Story(storyAsset.text);
            OnCreateStory?.Invoke(story);
            OnStoryCreated?.Invoke();
        }

        public void Begin()
        {
            if(running) { return; }

            transform.GetChild(0).gameObject.SetActive(true);
            running = true;
            
            OnStoryStarted?.Invoke();
            Progress();
        }

        public object GetVariable(string variableName)
            => story.variablesState[variableName];

        public void SetVariable(string variableName, object value)
            => story.variablesState[variableName] = value;

        public void BindFunction(string functionName, Action callback)
            => story.BindExternalFunction(functionName, callback);

        public void BindFunction<T1>(string functionName, Action<T1> callback)
            => story.BindExternalFunction(functionName, callback);

        public void BindFunction<T1, T2>(string functionName, Action<T1, T2> callback)
            => story.BindExternalFunction(functionName, callback);

        public void BindFunction<T1, T2, T3>(string functionName, Action<T1, T2, T3> callback)
            => story.BindExternalFunction(functionName, callback);

        public void BindFunction<T1, T2, T3, T4>(string functionName, Action<T1, T2, T3, T4> callback)
            => story.BindExternalFunction(functionName, callback);

        public void BindReturnFunction(string functionName, Func<object> callback)
            => story.BindExternalFunction(functionName, callback);

        public void BindReturnFunction<T1>(string functionName, Func<T1, object> callback)
            => story.BindExternalFunction(functionName, callback);

        public void BindReturnFunction<T1, T2>(string functionName, Func<T1, T2, object> callback)
            => story.BindExternalFunction(functionName, callback);

        public void BindReturnFunction<T1, T2, T3>(string functionName, Func<T1, T2, T3, object> callback)
            => story.BindExternalFunction(functionName, callback);

        public void BindReturnFunction<T1, T2, T3, T4>(string functionName, Func<T1, T2, T3, T4, object> callback)
            => story.BindExternalFunction(functionName, callback);

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
                
                currentReader.Process();
                if(currentReader.Busy) { yield return new WaitUntil(() => !currentReader.Busy); }
            }

            PresentChoices();
        }

        private IEnumerator WaitAndFinish()
        {
            currentReader.AddTask(new WaitForInputTask(currentReader.GetComponentInChildren<InputIndicator>()));
            
            currentReader.Process();
            if(currentReader.Busy) { yield return new WaitUntil(() => !currentReader.Busy); }
            
            OnStoryFinished?.Invoke();
            transform.GetChild(0).gameObject.SetActive(false);
            running = false;
        }

        private void PresentChoices()
        {
            if(story.currentChoices.Count < 1)
            {
                StartCoroutine(WaitAndFinish());
                return;
            }

            foreach (var choice in story.currentChoices)
            {
                choices.AddChoice(choice.text);
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
using System.Collections.Generic;
using System.Linq;
using EHT.Timeline.Visuals;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EHT.Timeline
{
    public class TimelineBeat : MonoBehaviour
    {
        public enum Completion
        {
            Locked,
            Uncompleted,
            Completed,
            Paradox
        }
        
        private HashSet<TimelineBeat> dependencies = new();
        private HashSet<TimelineBeat> dependents = new();
        
        private Character character;
        private Hour hour;

        [SerializeField] private Completion completionState;
        [SerializeField] private BeatState state;

        [SerializeField] private Color paradoxColor = Color.magenta;

        public Character Character
        {
            get => character;
            set
            {
                character = value;
                var color = character.Color;
                color.r *= 0.5f;
                color.g *= 0.5f;
                color.b *= 0.5f;
                image.color = color;
            }
        }

        public Hour Hour
        {
            get => hour;
            set
            {
                hour = value;
                label.text = $"{character?.name} {hour.name}";
                gameObject.name = label.text;
            }
        }
        
        public int Time { get; set; }

        public Completion CompletionStates => completionState;
        public bool Interactable => completionState != Completion.Locked && dependencies.All(dependency => dependency.completionState != Completion.Paradox);

        public BeatState State => state;

        public UnityEvent<TimelineBeat> OnBeatSelected;
        
        // TODO: Make this happen in a different class
        [SerializeField] private Image image;
        private TMP_Text label;
        
        private Button button;

        private void Awake()
        {
            label = GetComponentInChildren<TMP_Text>();
            
            button = GetComponentInChildren<Button>();
            button.onClick.AddListener(OnClick);
        }
        
        private void OnClick()
        {
            if(!Interactable) { return; }
            OnBeatSelected?.Invoke(this);
        }

        public void Unlock()
        {
            if(completionState != Completion.Locked) { return; }
            completionState = Completion.Uncompleted;
            image.color = character.Color;
        }

        public void Complete()
        {
            completionState = Completion.Completed;
            image.color = character.Color;
            foreach (var dependent in dependents)
            {
                dependent.MakeParadoxical();
            }
        }

        private void MakeParadoxical()
        {
            if(completionState != Completion.Completed) { return; }

            completionState = Completion.Paradox;
            image.color = paradoxColor;
            foreach (var dependent in dependents)
            {
                dependent.MakeParadoxical();
            }
        }

        public void InheritStateFromParents()
        {
            state.Reset();
            foreach (var dependency in dependencies)
            {
                state.Inherit(dependency.state);
            }
        }

        public void AddDependency(TimelineBeat parent)
        {
            if (!dependencies.Add(parent)) { return; }
            parent.AddDependent(this);
        }

        public void AddDependent(TimelineBeat child)
        {
            if (!dependents.Add(child)) { return; }
            child.AddDependency(this);
        }
    }
}
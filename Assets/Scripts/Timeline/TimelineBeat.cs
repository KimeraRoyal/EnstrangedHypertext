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
        
        private readonly HashSet<TimelineBeat> dependencies = new();
        private readonly HashSet<TimelineBeat> dependents = new();
        
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

        public UnityEvent<TimelineBeat> OnDependencyAdded;
        public UnityEvent<TimelineBeat> OnDependencyRemoved;
        
        public UnityEvent<TimelineBeat> OnDependentAdded;
        public UnityEvent<TimelineBeat> OnDependentRemoved;
        
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
            if(completionState == Completion.Locked) { return; }

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

        public void AddDependency(TimelineBeat parent, bool recurse = true)
        {
            if (!parent || !dependencies.Add(parent)) { return; }
            OnDependencyAdded?.Invoke(parent);
            if(recurse) { parent.AddDependent(this, false); }
        }

        public void RemoveDependency(TimelineBeat parent, bool recurse = true)
        {
            if(!dependencies.Remove(parent)) { return; }
            OnDependencyRemoved?.Invoke(parent);
            if (recurse) { parent.RemoveDependent(this, false); }
        }

        public void AddDependent(TimelineBeat child, bool recurse = true)
        {
            if (!child || !dependents.Add(child)) { return; }
            OnDependentAdded?.Invoke(child);
            if (recurse) { child.AddDependency(this, false); }
        }

        public void RemoveDependent(TimelineBeat child, bool recurse = true)
        {
            if(!dependents.Remove(child)) { return; }
            OnDependentRemoved?.Invoke(child);
            if (recurse) { child.RemoveDependency(this, false); }
        }

        public void ClearDependents()
        {
            foreach(var child in dependents)
            {
                OnDependentRemoved?.Invoke(child);
                child.RemoveDependency(this, false);
            }
            dependents.Clear();
        }
    }
}
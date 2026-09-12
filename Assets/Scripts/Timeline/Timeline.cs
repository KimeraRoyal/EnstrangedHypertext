using System;
using System.Collections.Generic;
using EHT.Narrative;
using UnityEngine;
using UnityEngine.Events;

namespace EHT.Timeline
{
    [RequireComponent(typeof(TimelineCharacters), typeof(TimelineHours), typeof(TimelineBeats))]
    public class Timeline : MonoBehaviour
    {
        private NarrativeSystem narrativeSystem;

        private TimelineCharacters characters;
        private TimelineHours hours;
        private TimelineBeats beats;

        private int currentTime = -1;
        private int maxVisitedTime = -1;

        public TimelineCharacters Characters
        {
            get
            {
                if (!characters) { characters = GetComponent<TimelineCharacters>(); }
                return characters;
            }
        }

        public TimelineHours Hours
        {
            get
            {
                if (!hours) { hours = GetComponent<TimelineHours>(); }
                return hours;
            }
        }
        
        public TimelineBeats Beats
        {
            get
            {
                if (!beats) { beats = GetComponent<TimelineBeats>(); }
                return beats;
            }
        }

        public int CurrentTime
        {
            get => currentTime;
            set
            {
                if(currentTime == value) { return; }
                currentTime = value;
                if (currentTime > maxVisitedTime)
                {
                    maxVisitedTime = currentTime;
                    beats.UnlockHour(maxVisitedTime);
                }
                maxVisitedTime = Mathf.Max(maxVisitedTime, currentTime);
                OnTimeChanged?.Invoke(currentTime);
            }
        }

        public int MaxVisitedTime => maxVisitedTime;

        public int MaxTime => hours.Count;

        public UnityEvent<int> OnTimeChanged;

        private void Awake()
        {
            narrativeSystem = FindAnyObjectByType<NarrativeSystem>();
            narrativeSystem.OnStoryFinished += StoryFinished;

            characters = GetComponent<TimelineCharacters>();
            hours = GetComponent<TimelineHours>();
            beats = GetComponent<TimelineBeats>();

            beats.IsBeatSelectable += IsBeatSelectable;
            beats.OnBeatSelected.AddListener(OnBeatSelected);
        }

        private void Start()
        {
            beats.ConstructTimelines(characters.Count, hours.Count);
            CurrentTime = 0;
        }

        private bool IsBeatSelectable(TimelineBeat beat)
            => beat.Time <= maxVisitedTime && !narrativeSystem.Running;

        private void OnBeatSelected(TimelineBeat beat)
        {
            if(!beat) { return; }
            CurrentTime = beat.Time;
            
            narrativeSystem.Create(beat.Hour.InkScript);
            narrativeSystem.SetVariable("character", beat.Character.name);
            narrativeSystem.Begin();
        }
        
        private void StoryFinished()
        {
            beats.CurrentBeat.Complete();
            CurrentTime++;

            beats.Deselect();
        }
    }
}

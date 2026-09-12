using System;
using UnityEngine;
using UnityEngine.Events;

namespace EHT.Timeline
{
    public class TimelineBeats : MonoBehaviour
    {
        private TimelineCharacters characters;
        private TimelineHours hours;
        
        private TimelineBeatSpawner beatSpawner;
        
        private TimelineBeat[,] characterTimelines;
        private TimelineBeat currentBeat;

        public TimelineBeat CurrentBeat
        {
            get => currentBeat;
            private set
            {
                if(currentBeat == value) { return; }
                currentBeat = value;
                OnBeatSelected?.Invoke(currentBeat);
            }
        }
        
        public UnityEvent<TimelineBeat> OnBeatSelected;

        public Func<TimelineBeat, bool> IsBeatSelectable;

        private void Awake()
        {
            characters = GetComponent<TimelineCharacters>();
            hours = GetComponent<TimelineHours>();
            
            beatSpawner = GetComponentInChildren<TimelineBeatSpawner>();
        }

        public void ConstructTimelines(int characterCount, int hourCount)
        {
            beatSpawner.ColumnCount = hourCount;
            beatSpawner.RowCount = characterCount;
            
            characterTimelines = new TimelineBeat[characterCount, hourCount];
            for (var character = 0; character < characterCount; character++)
            {
                for (var hour = 0; hour < hourCount; hour++)
                {
                    SpawnBeat(character, hour);
                }
            }
        }

        public void UnlockHour(int hour)
        {
            for (var character = 0; character < characters.Count; character++)
            {
                characterTimelines[character, hour].Unlock();
            }
        }

        public TimelineBeat GetBeat(int character, int hour)
        {
            if (character < 0 || character >= characters.Count)
            {
                Debug.LogError("Trying to get beat of invalid character.");
                return null;
            }
            if(hour < 0 || hour >= hours.Count) { return null; }
            return characterTimelines[character, hour];
        }

        public TimelineBeat GetBeat(Character character, int hour)
            => GetBeat(characters.IndexOf(character), hour);

        public TimelineBeat GetBeat(string character, int hour)
            => GetBeat(characters.IndexOf(character), hour);

        public void Select(TimelineBeat beat)
        {
            if (IsBeatSelectable != null && !IsBeatSelectable.Invoke(beat)) { return; }

            ResetInheritance(beat);
            CurrentBeat = beat;
        }

        public void Deselect()
            => CurrentBeat = null;

        private void SpawnBeat(int character, int hour)
        {
            var beat = beatSpawner.Spawn(hour, character);

            beat.Character = characters[character];
            beat.Hour = hours[hour];
            beat.Time = hour;
            beat.AddDependency(GetBeat(character, hour - 1));
            
            beat.OnBeatSelected.AddListener(Select);
            
            characterTimelines[character, hour] = beat;
        }

        private void ResetInheritance(TimelineBeat beat)
        {
            beat.InheritStateFromParents();
            
            beat.ClearDependents();
            beat.AddDependent(GetBeat(beat.Character, beat.Time + 1));
        }
    }
}
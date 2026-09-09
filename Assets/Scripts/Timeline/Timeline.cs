using System.Collections.Generic;
using EHT.Narrative;
using UnityEngine;
using UnityEngine.Events;

namespace EHT.Timeline
{
    public class Timeline : MonoBehaviour
    {
        private NarrativeSystem narrativeSystem;

        [SerializeField] private Character[] characters;
        [SerializeField] private Hour[] hours;

        private List<TimelineBeat>[] beats;
        [SerializeField] private TimelineBeat beatPrefab;
        private TimelineBeat currentBeat;

        private int maxTime = -1;
        private int currentTime = -1;

        // TODO: Move spawning beats into a new class
        [SerializeField] private float columnOffset = 1.0f, rowOffset = 1.0f;

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

        public int CurrentTime
        {
            get => currentTime;
            set
            {
                if(currentTime == value) { return; }
                currentTime = value;
                if (currentTime > maxTime)
                {
                    maxTime = currentTime;
                    UnlockHour(maxTime);
                }
                maxTime = Mathf.Max(maxTime, currentTime);
                OnHourChanged?.Invoke(currentTime);
            }
        }

        public UnityEvent<int> OnHourChanged;
        public UnityEvent<TimelineBeat> OnBeatSelected;

        private void Awake()
        {
            narrativeSystem = FindAnyObjectByType<NarrativeSystem>();
            narrativeSystem.OnStoryFinished += StoryFinished;
        }

        private void Start()
        {
            beats = new List<TimelineBeat>[characters.Length];
            for (var character = 0; character < characters.Length; character++)
            {
                beats[character] = new List<TimelineBeat>();
                for (var hour = 0; hour < hours.Length; hour++)
                {
                    SpawnBeat(character, hour);
                }
            }
            
            CurrentTime = 0;

            transform.position -= new Vector3((characters.Length - 1) * columnOffset, (hours.Length - 1) * rowOffset) / 2.0f;
        }

        private void SpawnBeat(int characterIndex, int hourIndex)
        {
            var offset = new Vector2(characterIndex * columnOffset, hourIndex * rowOffset);
            
            var timelineBeat = Instantiate(beatPrefab, transform.position + (Vector3) offset, Quaternion.identity, transform);
            timelineBeat.Character = characters[characterIndex];
            timelineBeat.Hour = hours[hourIndex];
            timelineBeat.Time = hourIndex;

            if (hourIndex > 0)
            {
                timelineBeat.AddDependency(beats[characterIndex][hourIndex - 1]);
            }
            
            timelineBeat.OnBeatSelected.AddListener(SelectBeat);
            
            beats[characterIndex].Add(timelineBeat);
        }

        private void SelectBeat(TimelineBeat beat)
        {
            if(beat.Time > maxTime || narrativeSystem.Running) { return; }

            CurrentBeat = beat;
            CurrentTime = CurrentBeat.Time;
            
            // TODO: Decouple this behaviour
            narrativeSystem.Create(beat.Hour.InkScript);
            narrativeSystem.SetVariable("character", beat.Character.name);
            narrativeSystem.Begin();
        }
        
        private void StoryFinished()
        {
            CurrentTime++;

            CurrentBeat.Complete();
            CurrentBeat = null;
        }

        private void UnlockHour(int hour)
        {
            for (var character = 0; character < characters.Length; character++)
            {
                beats[character][hour].Unlock();
            }
        }
    }
}

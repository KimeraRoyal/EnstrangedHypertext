using System.Collections.Generic;
using EHT.Narrative;
using UnityEngine;
using UnityEngine.Events;

namespace EHT.Timeline
{
    public class Timeline : MonoBehaviour
    {
        private const int MAX_ROWS = 20;
        
        private NarrativeSystem narrativeSystem;

        [SerializeField] private Character[] characters;
        [SerializeField] private Hour[] hours;

        private List<TimelineBeat> beats = new();
        [SerializeField] private TimelineBeat beatPrefab;
        private TimelineBeat currentBeat;

        [SerializeField] private int maxTime;
        [SerializeField] private int currentTime;

        [SerializeField] private float columnOffset = 1.0f, rowOffset = 1.0f;

        public TimelineBeat CurrentBeat => currentBeat;

        public int CurrentTime
        {
            get => currentTime;
            set
            {
                if(currentTime == value) { return; }
                currentTime = value;
                OnHourChanged?.Invoke(currentTime);
            }
        }

        public UnityEvent<int> OnHourChanged;
        public UnityEvent<TimelineBeat> OnBeatSelected;

        private void Awake()
        {
            narrativeSystem = FindAnyObjectByType<NarrativeSystem>();
        }

        private void Start()
        {
            for (var character = 0; character < characters.Length; character++)
            {
                for (var hour = 0; hour < hours.Length; hour++)
                {
                    SpawnBeat(character, hour);
                }
            }
            
            maxTime = hours.Length;
            currentTime = 0;

            transform.position -= new Vector3((characters.Length - 1) * columnOffset, (hours.Length - 1) * rowOffset) / 2.0f;
        }

        private void SpawnBeat(int characterIndex, int hourIndex)
        {
            var offset = new Vector2(characterIndex * columnOffset, hourIndex * rowOffset);
            
            var timelineBeat = Instantiate(beatPrefab, transform.position + (Vector3) offset, Quaternion.identity, transform);
            timelineBeat.Character = characters[characterIndex];
            timelineBeat.Hour = hours[hourIndex];
            timelineBeat.Time = hourIndex;
            
            timelineBeat.OnBeatSelected.AddListener(SelectBeat);
            
            beats.Add(timelineBeat);
        }

        private void SelectBeat(TimelineBeat beat)
        {
            if(beat.Time != currentTime || narrativeSystem.Running) { return; }
            CurrentTime++;

            currentBeat = beat;
            OnBeatSelected?.Invoke(beat);

            // TODO: Decouple this behaviour
            narrativeSystem.Create(beat.Hour.InkScript);
            narrativeSystem.SetVariable("character", beat.Character.name);
            narrativeSystem.Begin();
            
            // TODO: Clear current beat on narrative system finish
        }
    }
}

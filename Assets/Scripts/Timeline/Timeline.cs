using System.Collections.Generic;
using EHT.Narrative;
using UnityEngine;

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

        [SerializeField] private int maxTime;
        [SerializeField] private int currentTime;

        [SerializeField] private float columnOffset = 1.0f, rowOffset = 1.0f;

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
            
            timelineBeat.OnBeatSelected.AddListener(OnBeatSelected);
            
            beats.Add(timelineBeat);
        }

        private void OnBeatSelected(TimelineBeat beat)
        {
            if(beat.Time != currentTime || narrativeSystem.Running) { return; }
            Debug.Log(beat.Hour.name);
            currentTime++;

            narrativeSystem.Create(beat.Hour.InkScript);
            narrativeSystem.SetVariable("character", beat.Character.name);
            narrativeSystem.Begin();
        }
    }
}

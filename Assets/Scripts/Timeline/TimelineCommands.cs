using EHT.Narrative;
using UnityEngine;

namespace EHT.Timeline
{
    public class TimelineCommands : MonoBehaviour
    {
        private NarrativeSystem narrative;
        private Timeline timeline;
        
        private void Awake()
        {
            narrative = FindAnyObjectByType<NarrativeSystem>();
            timeline = FindAnyObjectByType<Timeline>();
            
            narrative.OnStoryCreated += OnStoryCreated;
        }

        private void OnStoryCreated()
        {
            narrative.BindFunction<string>("setMemory", SetMemory);
            narrative.BindReturnFunction<string>("evaluateMemory", EvaluateMemory);
        }

        private void SetMemory(string id)
        {
            if (!timeline.CurrentBeat) { return; }
            timeline.CurrentBeat.State.CharacterState.AddMemory(id.ToLower());
        }

        private object EvaluateMemory(string id)
        {
            if (!timeline.CurrentBeat) { return false; }
            return timeline.CurrentBeat.State.CharacterState.HasMemory(id.ToLower());
        }
    }
}
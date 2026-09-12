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
            
            narrative.BindFunction<string>("involveCharacter", InvolveCharacter);
        }

        private void SetMemory(string id)
        {
            if (!timeline.Beats.CurrentBeat) { return; }
            timeline.Beats.CurrentBeat.State.CharacterState.AddMemory(id.ToLower());
        }

        private object EvaluateMemory(string id)
        {
            if (!timeline.Beats.CurrentBeat) { return false; }
            return timeline.Beats.CurrentBeat.State.CharacterState.HasMemory(id.ToLower());
        }

        private void InvolveCharacter(string id)
        {
            timeline.Beats.CurrentBeat.AddDependent(timeline.Beats.GetBeat(id, timeline.Beats.CurrentBeat.Time + 1));
        }
    }
}
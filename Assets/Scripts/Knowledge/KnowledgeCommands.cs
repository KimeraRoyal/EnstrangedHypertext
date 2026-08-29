using System;
using EHT.Narrative;
using EHT.Narrative.Reader;
using EHT.Narrative.Reader.Commands;
using UnityEngine;

namespace EHT.Knowledge
{
    public class KnowledgeCommands : CommandProvider
    {
        private NarrativeSystem narrative;
        private KnowledgeWindows windows;
        
        protected override void Awake()
        {
            narrative = FindAnyObjectByType<NarrativeSystem>();
            windows = FindAnyObjectByType<KnowledgeWindows>();
            
            narrative.OnStoryCreated += OnStoryCreated;
            
            base.Awake();
        }

        protected override void OnRegisterCommands()
        {
            RegisterCommand("know", KnowledgeEmbed);
            RegisterCommand("knev", EvaluateKnowledge);
        }

        private void OnStoryCreated()
        {
            narrative.BindReturnFunction<string>("evaluateKnowledge", EvaluateKnowledge);
            Debug.Log("Bound function evaluateKnowledge");
        }

        private ReaderTask KnowledgeEmbed(string[] arguments)
        {
            if (arguments.Length < 1) { return new EndKnowledgeEmbedTask(); }
            return new BeginKnowledgeEmbedTask(arguments[0]);
        }

        private ReaderTask EvaluateKnowledge(string[] arguments)
        {
            if (arguments.Length < 2) { return null; }
            return new EvaluateKnowledgeTask(narrative, windows, arguments[0], arguments[1]);
        }

        private object EvaluateKnowledge(string id)
        {
            Debug.Log($"Evaluate Knowledge: {id}");
            return windows.IsWindowOpen(id);
        }
    }
}

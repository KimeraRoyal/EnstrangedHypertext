using EHT.Narrative;
using EHT.Narrative.Reader;

namespace EHT.Knowledge.Windows
{
    public class BeginKnowledgeEmbedTask : ReaderTask
    {
        private readonly string id;
        
        public BeginKnowledgeEmbedTask(string id)
        {
            this.id = id;
        }
        
        protected override void Execute()
        {
            Typewriter.WriteLine($"<link=\"{id}\"><u>");
            Completed = true;
        }
    }

    public class EndKnowledgeEmbedTask : ReaderTask
    {
        protected override void Execute()
        {
            Typewriter.WriteLine("</u></link>");
            Completed = true;
        }
    }

    public class EvaluateKnowledgeTask : ReaderTask
    {
        private readonly NarrativeSystem narrative;
        private readonly KnowledgeWindows knowledge;

        private readonly string id;
        private readonly string inkVariable;
        
        public EvaluateKnowledgeTask(NarrativeSystem narrative, KnowledgeWindows knowledge, string id, string inkVariable)
        {
            this.narrative = narrative;
            this.knowledge = knowledge;

            this.id = id;
            this.inkVariable = inkVariable;
        }

        protected override void Execute()
        {
            narrative.SetVariable(inkVariable, knowledge.IsWindowOpen(id));
            Completed = true;
        }
    }
}
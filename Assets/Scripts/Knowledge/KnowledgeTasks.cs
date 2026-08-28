using EHT.Narrative.Reader;

namespace EHT.Knowledge
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
}
namespace EHT.Narrative.Reader
{
    public class ReadLineTask : ReaderTask
    {
        private Typewriter typewriter;

        private string line;

        public ReadLineTask(Typewriter typewriter, string line)
        {
            this.typewriter = typewriter;
            this.line = line;    
        }

        protected override void Execute()
        {
            typewriter.WriteLine(line);
            Completed = true;
        }
    }

    public class NewLineTask : ReaderTask
    {
        private Typewriter typewriter;

        public NewLineTask(Typewriter typewriter)
        {
            this.typewriter = typewriter;
        }

        protected override void Execute()
        {
            typewriter.NewLine();
            Completed = true;
        }
    }

    public class ClearLinesTask : ReaderTask
    {
        private Typewriter typewriter;

        public ClearLinesTask(Typewriter typewriter)
        {
            this.typewriter = typewriter;
        }

        protected override void Execute()
        {
            typewriter.ClearLines();
            Completed = true;
        }
    }
}
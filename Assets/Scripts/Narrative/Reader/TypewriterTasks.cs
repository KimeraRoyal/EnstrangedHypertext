namespace EHT.Narrative.Reader
{
    public class ReadLineTask : ReaderTask
    {
        private readonly string line;

        public ReadLineTask(string line)
        {
            this.line = line;    
        }

        protected override void Execute()
        {
            Typewriter.WriteLine(line);
            Completed = true;
        }
    }

    public class NewLineTask : ReaderTask
    {
        protected override void Execute()
        {
            Typewriter.NewLine();
            Completed = true;
        }
    }

    public class ClearLinesTask : ReaderTask
    {
        protected override void Execute()
        {
            Typewriter.ClearLines();
            Completed = true;
        }
    }
}
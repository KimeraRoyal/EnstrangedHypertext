using UnityEngine;

namespace EHT.Narrative.Reader
{
    public abstract class ReaderTask
    {
        public Typewriter Typewriter { get; set; }
        
        public bool Working { get; private set; }

        public bool Completed { get; protected set; }

        public ReaderTask() { }

        public void BeginWork()
        {
            if(Working || Completed) { return; }
            Working = true;
            Execute();
        }

        protected abstract void Execute();
    }

    public class DebugTask : ReaderTask
    {
        private string output;

        public DebugTask(string output)
        {
            this.output = output;    
        }

        protected override void Execute()
        {
            Debug.Log(output);
            Completed = true;
        }
    }
}
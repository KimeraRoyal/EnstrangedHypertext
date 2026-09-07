using System;
using System.Collections;
using System.Collections.Generic;
using EHT.Narrative.Reader.Commands;
using UnityEngine;
 
namespace EHT.Narrative.Reader
{
    public class Reader : MonoBehaviour
    {
        private Typewriter typewriter;
        private CommandProcessor commandProcessor;

        private readonly Queue<ReaderTask> tasks = new();

        private bool busy;
        private int blocking;

        public bool Busy => busy || blocking > 0;

        private void Awake()
        {
            typewriter = GetComponentInChildren<Typewriter>();
            commandProcessor = GetComponentInChildren<CommandProcessor>();

            commandProcessor.RegisterCommand("nwln", _ => new NewLineTask());
            commandProcessor.RegisterCommand("cler", _ => new ClearLinesTask());
        }

        public void DecodeLine(string line)
        {
            tasks.Enqueue(new NewLineTask(true));

            var components = line.Split(new[] { '[', ']' });
            for(var i = 0; i < components.Length; i++)
            {
                if(components[i].Length < 1) { continue; }
                if(i % 2 == 1)
                {
                    DecodeCommand(components[i]);
                    continue;
                }
                tasks.Enqueue(new ReadLineTask(components[i]));
            }
        }

        public void ClearLines()
        {
            tasks.Enqueue(new ClearLinesTask());
        }

        public void AddTask(ReaderTask task)
        {
            tasks.Enqueue(task);
        }

        public void Process()
        {
            busy = true;
            ProcessNext();
        }

        public void Block()
            => blocking++;

        public void Unblock()
            => blocking--;

        private void FinishProcessing()
        {
            busy = false;
        }

        private void ProcessNext()
        {
            if(typewriter.Busy)
            {
                StartCoroutine(WaitForTypewriter());
                return;
            }

            if(!tasks.TryDequeue(out var task))
            {
                FinishProcessing();
                return;
            }
            
            task.Typewriter = typewriter;
            task.BeginWork();
            
            if(task.Completed)
            {
                ProcessNext();
                return;
            }
            StartCoroutine(WaitForTask(task));
        }

        private IEnumerator WaitForTypewriter()
        {
            yield return new WaitUntil(() => !typewriter.Busy);
            ProcessNext();
        }

        private IEnumerator WaitForTask(ReaderTask task)
        {
            yield return new WaitUntil(() => task.Completed);
            ProcessNext();
        }

        private void DecodeCommand(string text)
        {
            var components = text.Split('=');
            if(components.Length < 1) { return; }
            
            var arguments = Array.Empty<string>();
            if(components.Length > 1) { arguments = components[1].Split(','); }

            var task = commandProcessor.Process(components[0], arguments);
            if(task == null) { return; }
            tasks.Enqueue(task);
        }
    }
}
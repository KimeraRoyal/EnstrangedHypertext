using System;
using UnityEngine;

namespace EHT.Narrative.Reader.Commands
{
    public abstract class CommandProvider : MonoBehaviour
    {
        private CommandProcessor commandProcessor;

        protected virtual void Awake()
        {
            commandProcessor = FindAnyObjectByType<CommandProcessor>(); 
            OnRegisterCommands();
        }

        protected void RegisterCommand(string command, Func<string[], ReaderTask> commandDelegate)
            => commandProcessor.RegisterCommand(command, commandDelegate);

        protected abstract void OnRegisterCommands();
    }
}
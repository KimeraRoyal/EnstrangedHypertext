using System;
using System.Collections.Generic;
using UnityEngine;

namespace EHT.Narrative.Reader.Commands
{
    public class CommandProcessor : MonoBehaviour
    {
        private readonly Dictionary<string, Func<string[], ReaderTask>> commands = new();

        public ReaderTask Process(string command, string[] arguments)
        {
            if(!commands.TryGetValue(command.ToLower(), out var commandDelegate)) { return null; }
            return commandDelegate.Invoke(arguments);
        }   

        public void RegisterCommand(string command, Func<string[], ReaderTask> commandDelegate)
        {
            commands.Add(command.ToLower(), commandDelegate);
        }
    }
}
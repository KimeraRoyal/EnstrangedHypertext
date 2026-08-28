
using EHT.Narrative.Reader.Commands;
using UnityEngine;

namespace EHT.Narrative.Reader.Wait
{
    [RequireComponent(typeof(InputIndicator))]
    public class WaitCommands : CommandProvider
    {
        private InputIndicator indicator;

        protected override void Awake()
        {
            indicator = GetComponent<InputIndicator>();
            
            base.Awake();
        }

        protected override void OnRegisterCommands()
        {
            RegisterCommand("wait", WaitForSeconds);
            RegisterCommand("wafi", WaitForInput);
        }

        private ReaderTask WaitForSeconds(string[] arguments)
        {
            if(arguments.Length < 1 || !float.TryParse(arguments[0], out var duration)) { return null; }
            return new WaitForSecondsTask(this, duration);
        }

        private ReaderTask WaitForInput(string[] arguments)
        {
            return new WaitForInputTask(indicator);
        }
    }
}

using UnityEngine;

namespace EHT.Narrative.Reader.Commands.Wait
{
    [RequireComponent(typeof(InputIndicator))]
    public class WaitCommands : MonoBehaviour
    {
        private InputIndicator indicator;

        private void Awake()
        {
            indicator = GetComponent<InputIndicator>();

            var commandProcessor = FindAnyObjectByType<CommandProcessor>();
            
            commandProcessor.RegisterCommand("wait", WaitForSeconds);
            commandProcessor.RegisterCommand("wafi", WaitForInput);
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
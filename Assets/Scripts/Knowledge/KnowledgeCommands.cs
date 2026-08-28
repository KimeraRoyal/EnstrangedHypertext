using EHT.Narrative.Reader;
using EHT.Narrative.Reader.Commands;
using UnityEngine;

namespace EHT.Knowledge
{
    public class KnowledgeCommands : CommandProvider
    {
        protected override void OnRegisterCommands()
        {
            Debug.Log("Register Knowledge Embed");
            RegisterCommand("know", KnowledgeEmbed);
        }

        private ReaderTask KnowledgeEmbed(string[] arguments)
        {
            Debug.Log("Knowledge Embed");
            if (arguments.Length < 1) { return new EndKnowledgeEmbedTask(); }
            return new BeginKnowledgeEmbedTask(arguments[0]);
        }
    }
}

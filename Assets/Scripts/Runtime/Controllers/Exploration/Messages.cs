using Akashic.Core.Messages;
using Akashic.ScriptableObjects.Exploration;

namespace Akashic.Runtime.Controllers.Exploration
{
    internal sealed class LoadExplorationEnvironmentMessage : IMessage
    {
        public readonly ExplorationEnvironmentData ExplorationEnvironmentToLoad;
        
        public LoadExplorationEnvironmentMessage(ExplorationEnvironmentData explorationEnvironmentToLoad)
        {
            ExplorationEnvironmentToLoad = explorationEnvironmentToLoad;
        }
    }

    internal sealed class LoadExplorationEnvironmentFromTriggerMessage : IMessage
    {
        public readonly ExplorationEnvironmentData ExplorationEnvironmentToLoad;
        public readonly string TriggerId;

        public LoadExplorationEnvironmentFromTriggerMessage(
            ExplorationEnvironmentData explorationEnvironmentToLoad,
            string triggerId
        )
        {
            ExplorationEnvironmentToLoad = explorationEnvironmentToLoad;
            TriggerId = triggerId;
        }
    }
}
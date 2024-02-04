using Akashic.Core.Messages;
using Akashic.ScriptableObjects.Exploration;

namespace Akashic.Runtime.Builders.Exploration
{
    internal sealed class LoadNewExplorationSceneMessage : IMessage
    {
        public readonly ExplorationEnvironmentData ExplorationEnvironmentToLoad;
        
        public LoadNewExplorationSceneMessage(ExplorationEnvironmentData explorationEnvironmentToLoad)
        {
            ExplorationEnvironmentToLoad = explorationEnvironmentToLoad;
        }
    }
}
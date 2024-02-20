using Akashic.Runtime.Actors.Exploration;
using UnityEngine;

namespace Akashic.ScriptableObjects.Exploration
{
    [CreateAssetMenu(menuName = "Akashic/Exploration/New Exploration Scene")]
    internal sealed class ExplorationEnvironmentData : ScriptableObject
    {
        public string roomName;
        public string roomDescription;
        public string roomId;
        public ExplorationEnvironment explorationEnvironment;
    }
}
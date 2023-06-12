using Akashic.Core;
using UnityEditor;

namespace Akashic.Runtime.MonoSystems.Scene
{
    /// <summary>
    /// Loads a scene in the game.
    /// </summary>
    internal interface ISceneManagementMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Loads a scene based on name with an optional parameter of SceneType (defaults to current scene type)
        /// </summary>
        public void LoadScene(SceneAsset scene);
    }
}

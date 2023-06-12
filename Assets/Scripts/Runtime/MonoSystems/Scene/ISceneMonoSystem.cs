using Akashic.Core;
using UnityEditor;

namespace Akashic.Runtime.MonoSystems.Scene
{
    /// <summary>
    /// Loads a scene in the game.
    /// </summary>
    internal interface ISceneMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Loads a scene.
        /// </summary>
        public void LoadScene(SceneAsset scene);
    }
}

using Akashic.Core;
using UnityEditor;

namespace Akashic.Runtime.MonoSystems.Scene
{
    internal interface ISceneMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Loads the Main Menu.
        /// </summary>
        public void LoadMainMenuScene();

        /// <summary>
        /// Loads the exploration scene.
        /// </summary>
        public void LoadExplorationScene();

        /// <summary>
        /// Loads the battle scene.
        /// </summary>
        public void LoadBattleScene();
    }
}

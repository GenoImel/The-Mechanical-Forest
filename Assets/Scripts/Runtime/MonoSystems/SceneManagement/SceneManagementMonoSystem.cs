using Akashic.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Akashic.Runtime.MonoSystems.SceneManagement
{
    /// <summary>
    /// Holds possible types of states of an individual scene
    /// Current Types are:
    /// Dungeon
    /// Battle
    /// MainMenu
    /// </summary>
    public enum SceneType 
    {
        Dungeon,
        Battle,
        MainMenu,
        None
    }

    /// <summary>
    /// Static class that handles Scene Loading and state management
    /// </summary>
    internal sealed class SceneManagementMonoSystem : MonoBehaviour, ISceneManagementMonoSystem
    {
        /// <summary>
        /// Refers to the current type of scene loaded. Defaults to None.
        /// </summary>
        public static SceneType CurrentSceneType { get; private set; } = SceneType.None;

        /// <summary>
        /// Refers to the name of the current scene
        /// </summary>
        public static string CurrentScene { get; private set; }

        /// <summary>
        /// Loads a scene based on name with an optional parameter of SceneType (defaults to current scene type)
        /// </summary>
        public void LoadScene(string sceneName, SceneType sceneType = SceneType.None) 
        {
            if (CurrentScene == sceneName)
            {
                return;
            }

            CurrentSceneType = (sceneType == SceneType.None) ? CurrentSceneType : sceneType;
            CurrentScene = sceneName;
            
            GameManager.Publish(new SceneLoadedMessage(CurrentSceneType, sceneName));

            //TO DO: Make this async or coroutine.
            SceneManager.LoadScene(CurrentScene);
        }

        /// <summary>
        /// Boolean function that returns true if parameter matches with current scene type
        /// </summary>
        public bool IsSceneType(SceneType typeQuery)
        {
            return typeQuery == CurrentSceneType;
        }

        /// <summary>
        /// Returns the reference to this script (Apart of the ISceneManagementMonoSystem interface)
        /// </summary>
        public SceneManagementMonoSystem GetScript() {
            return this;
        }
    }
}

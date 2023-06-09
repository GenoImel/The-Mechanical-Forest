using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace Akashic.Core
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
        NONE
    }

    /// <summary>
    /// Static class that handle's Scene Loading and state management
    /// </summary>
    public class SceneManagement : MonoBehaviour, ISceneManagement
    {
        //  Refers to the current type of scene loaded
        //  Defaults to NONE
        public static SceneType currentSceneType { get; private set; } = SceneType.NONE;

        //  Refers to the name of the current scene
        public static string currentScene { get; private set; }

        /// <summary>
        /// Loads a scene based on name with an optional parameter of SceneType (defaults to current scene type)
        /// </summary>
        public void LoadScene(string sceneName, SceneType sceneType = SceneType.NONE) 
        {
            if (currentScene == sceneName) return;

            currentSceneType = (sceneType == SceneType.NONE) ? currentSceneType : sceneType;
            currentScene = sceneName;

            SceneMessage message = new SceneMessage(currentSceneType, sceneName);
            GameManager.Publish(message);

            SceneManager.LoadScene(currentScene);
        }

        /// <summary>
        /// Boolean function that returns true if parameter matches with current scene type
        /// </summary>
        public bool IsSceneType(SceneType typeQuery)
        {
            return typeQuery == currentSceneType;
        }
    }

    /// <summary>
    /// Holds Scene Data to be passed onto the built in Message Interface
    /// </summary>
    internal sealed class SceneMessage : IMessage
    {
        // Loaded Sceen Type
        public SceneType sceneType;
        // Loaded Scene Name
        public string sceneName;

        // standard constructor for type SceneMessage
        public SceneMessage(SceneType type, string name) 
        {
            sceneType = type;
            sceneName = name;
        }
    }
}

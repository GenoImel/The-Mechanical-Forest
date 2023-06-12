using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.SceneManagement
{
    /// <summary>
    /// Indicates that a scene has been loaded.
    /// </summary>
    internal sealed class SceneLoadedMessage : IMessage
    {
        public SceneType Type { get; }
        public string SceneName { get; }
        
        /// <param name="sceneType">Type of scene loaded.</param>
        /// <param name="sceneName">Name of the loaded scene.</param>
        public SceneLoadedMessage(SceneType sceneType, string sceneName) 
        {
            Type = sceneType;
            SceneName = sceneName;
        }
    }
}
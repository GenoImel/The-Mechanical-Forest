using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.SceneManagement
{
    /// <summary>
    /// Holds Scene Data to be passed onto the built in Message Interface
    /// </summary>
    internal sealed class SceneLoadedMessage : IMessage
    {
        // Loaded Scene Type
        public SceneType SceneSceneType;
        // Loaded Scene Name
        public string SceneSceneName;

        // standard constructor for type SceneMessage
        public SceneLoadedMessage(SceneType sceneType, string sceneName) 
        {
            SceneSceneType = sceneType;
            SceneSceneName = sceneName;
        }
    }
}
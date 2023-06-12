using Akashic.Core;
using UnityEditor;

namespace Akashic.Runtime.MonoSystems.Scene
{
    /// <summary>
    /// Holds Scene Data to be passed onto the built in Message Interface
    /// </summary>
    internal sealed class SceneLoadedMessage : IMessage
    {
        public SceneAsset SceneName { get; }
        
        public SceneLoadedMessage(SceneAsset scene) 
        {
            SceneName = scene;
        }
    }
}
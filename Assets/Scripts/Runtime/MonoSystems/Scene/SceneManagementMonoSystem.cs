using Akashic.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Akashic.Runtime.MonoSystems.Scene
{
    internal sealed class SceneManagementMonoSystem : MonoBehaviour, ISceneManagementMonoSystem
    {
        private string currentScene;

        private void Awake()
        {
            currentScene = SceneManager.GetActiveScene().name;
        }
        
        public void LoadScene(SceneAsset scene) 
        {
            if (currentScene == scene.name)
            {
                return;
            }
            
            currentScene = scene.name;
            
            GameManager.Publish(new SceneLoadedMessage(scene));

            //TO DO: Make this async or coroutine.
            SceneManager.LoadScene(currentScene);
        }
    }
}

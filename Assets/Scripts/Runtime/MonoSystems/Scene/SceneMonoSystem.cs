using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Akashic.Runtime.MonoSystems.Scene
{
    internal sealed class SceneMonoSystem : MonoBehaviour, ISceneMonoSystem
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

            //TO DO: Make this async or coroutine.[ser
            SceneManager.LoadScene(currentScene);
        }
    }
}

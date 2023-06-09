using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime
{
    internal sealed class AkashicGameManager : GameManager
    {
        [Header("Management")] 
        [SerializeField] private Transform monoSystemsParentTransform;
        
        [SerializeField] private Transform controllerParentTransform;

        [Header("MonoSystem References:")]

        [SerializeField] private SceneManagement sceneManagementMonoSystem;

        protected override string GetApplicationName()
        {
            return nameof(AkashicGameManager);
        }

        protected override void OnInitialized()
        {
            monoSystemsParentTransform.gameObject.SetActive(true);
            controllerParentTransform.gameObject.SetActive(true);

            AddMonoSystem<SceneManagement, ISceneManagement>(sceneManagementMonoSystem);
        }
    }
}
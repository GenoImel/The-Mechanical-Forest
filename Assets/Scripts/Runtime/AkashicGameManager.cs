using Akashic.Core;
using Akashic.Runtime.MonoSystems.Scene;
using Akashic.Runtime.MonoSystems.Sound;
using UnityEngine;

namespace Akashic.Runtime
{
    internal sealed class AkashicGameManager : GameManager
    {
        [Header("Management")] 
        [SerializeField] private Transform monoSystemsParentTransform;
        
        [SerializeField] private Transform controllerParentTransform;

        [Header("MonoSystems:")]
        [SerializeField] private SceneManagementMonoSystem sceneManagementMonoSystem;
        
        [SerializeField] private SoundMonoSystem soundMonoSystem;

        protected override string GetApplicationName()
        {
            return nameof(AkashicGameManager);
        }

        protected override void OnInitialized()
        {
            monoSystemsParentTransform.gameObject.SetActive(true);
            controllerParentTransform.gameObject.SetActive(true);

            AddMonoSystem<SceneManagementMonoSystem, ISceneManagementMonoSystem>(sceneManagementMonoSystem);
            AddMonoSystem<SoundMonoSystem, ISoundMonoSystem>(soundMonoSystem);
        }
    }
}
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
        
        [Header("MonoSystems")]
        [SerializeField] private SceneMonoSystem sceneMonoSystem;
        
        [SerializeField] private SoundMonoSystem soundMonoSystem;

        protected override string GetApplicationName()
        {
            return nameof(AkashicGameManager);
        }

        protected override void OnInitialized()
        {
            BootstrapMonoSystems();
                
            monoSystemsParentTransform.gameObject.SetActive(true);
            controllerParentTransform.gameObject.SetActive(true);
        }

        private void BootstrapMonoSystems()
        {
            AddMonoSystem<SceneMonoSystem, ISceneMonoSystem>(sceneMonoSystem);
            AddMonoSystem<SoundMonoSystem, ISoundMonoSystem>(soundMonoSystem);
        }
    }
}
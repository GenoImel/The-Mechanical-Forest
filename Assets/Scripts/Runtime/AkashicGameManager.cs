using Akashic.Core;
using Akashic.Runtime.MonoSystems.BattleStates;
using Akashic.Runtime.MonoSystems.GameStates;
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
        [SerializeField] private GameStateMonoSystem gameStateMonoSystem;
        [SerializeField] private BattleStateMonoSystem battleStateMonoSystem;
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
            AddMonoSystem<GameStateMonoSystem, IGameStateMonoSystem>(gameStateMonoSystem);
            AddMonoSystem<BattleStateMonoSystem, IBattleStateMonoSystem>(battleStateMonoSystem);
            AddMonoSystem<SceneMonoSystem, ISceneMonoSystem>(sceneMonoSystem);
            AddMonoSystem<SoundMonoSystem, ISoundMonoSystem>(soundMonoSystem);
        }
    }
}
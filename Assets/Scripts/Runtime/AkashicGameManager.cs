using Akashic.Core;
using Akashic.Runtime.MonoSystems.BattleStates;
using Akashic.Runtime.MonoSystems.Dialogue;
using Akashic.Runtime.MonoSystems.ExplorationStates;
using Akashic.Runtime.MonoSystems.GameStates;
using Akashic.Runtime.MonoSystems.Party;
using Akashic.Runtime.MonoSystems.PlayerPrefs;
using Akashic.Runtime.MonoSystems.Save;
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
        [SerializeField] private ExplorationStateMonoSystem explorationStateMonoSystem;
        [SerializeField] private SceneMonoSystem sceneMonoSystem;
        [SerializeField] private SoundMonoSystem soundMonoSystem;
        [SerializeField] private PartyMonoSystem partyMonoSystem;
        [SerializeField] private PlayerPreferencesMonoSystem playerPreferencesMonoSystem;
        [SerializeField] private SaveMonoSystem saveMonoSystem;
        [SerializeField] private DialogueMonoSystem dialogueMonoSystem;

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
            AddMonoSystem<ExplorationStateMonoSystem, IExplorationStateMonoSystem>(explorationStateMonoSystem);
            AddMonoSystem<SceneMonoSystem, ISceneMonoSystem>(sceneMonoSystem);
            AddMonoSystem<SoundMonoSystem, ISoundMonoSystem>(soundMonoSystem);
            AddMonoSystem<PartyMonoSystem, IPartyMonoSystem>(partyMonoSystem);
            AddMonoSystem<PlayerPreferencesMonoSystem, IPlayerPreferencesMonoSystem>(playerPreferencesMonoSystem);
            AddMonoSystem<SaveMonoSystem, ISaveMonoSystem>(saveMonoSystem);
            AddMonoSystem<DialogueMonoSystem, IDialogueMonoSystem>(dialogueMonoSystem);
        }
    }
}
using Akashic.Core;
using Akashic.Runtime.MonoSystems.BattleStates;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.Debugger;
using Akashic.Runtime.MonoSystems.ExplorationStates;
using Akashic.Runtime.MonoSystems.GameStates;
using Akashic.Runtime.MonoSystems.Party;
using Akashic.Runtime.MonoSystems.PlayerPrefs;
using Akashic.Runtime.MonoSystems.Save;
using Akashic.Runtime.MonoSystems.Scene;
using Akashic.Runtime.MonoSystems.Sound;
using Akashic.Runtime.MonoSystems.Story;
using UnityEngine;

namespace Akashic.Runtime
{
    internal sealed class AkashicGameManager : GameManager
    {
        [Header("Management")] 
        [SerializeField] private Transform stateMachinesParentTransform;
        
        [SerializeField] private Transform monoSystemsParentTransform;
        
        [SerializeField] private Transform controllersParentTransform;
        
        //[Header("StateMachines")]

        [Header("MonoSystems")]
        [SerializeField] private GameStateMonoSystem gameStateMonoSystem;
        [SerializeField] private BattleStateMonoSystem battleStateMonoSystem;
        [SerializeField] private ExplorationStateMonoSystem explorationStateMonoSystem;
        [SerializeField] private SceneMonoSystem sceneMonoSystem;
        [SerializeField] private SoundMonoSystem soundMonoSystem;
        [SerializeField] private PartyMonoSystem partyMonoSystem;
        [SerializeField] private ConfigMonoSystem configMonoSystem;
        [SerializeField] private PlayerPreferencesMonoSystem playerPreferencesMonoSystem;
        [SerializeField] private SaveMonoSystem saveMonoSystem;
        [SerializeField] private StoryMonoSystem storyMonoSystem;
        [SerializeField] private DebuggerMonoSystem debuggerMonoSystem;

        protected override string GetApplicationName()
        {
            return nameof(AkashicGameManager);
        }

        protected override void OnInitialized()
        {
            InitializeGameStateMachines();
            InitializeGameMonoSystems();
                
            SetParentsActive();
        }

        protected override void InitializeGameStateMachines()
        {
        }
        
        protected override void InitializeGameMonoSystems()
        {
            AddMonoSystem<GameStateMonoSystem, IGameStateMonoSystem>(gameStateMonoSystem);
            AddMonoSystem<BattleStateMonoSystem, IBattleStateMonoSystem>(battleStateMonoSystem);
            AddMonoSystem<ExplorationStateMonoSystem, IExplorationStateMonoSystem>(explorationStateMonoSystem);
            AddMonoSystem<SceneMonoSystem, ISceneMonoSystem>(sceneMonoSystem);
            AddMonoSystem<SoundMonoSystem, ISoundMonoSystem>(soundMonoSystem);
            AddMonoSystem<PartyMonoSystem, IPartyMonoSystem>(partyMonoSystem);
            AddMonoSystem<ConfigMonoSystem, IConfigMonoSystem>(configMonoSystem);
            AddMonoSystem<PlayerPreferencesMonoSystem, IPlayerPreferencesMonoSystem>(playerPreferencesMonoSystem);
            AddMonoSystem<SaveMonoSystem, ISaveMonoSystem>(saveMonoSystem);
            AddMonoSystem<StoryMonoSystem, IStoryMonoSystem>(storyMonoSystem);
            AddMonoSystem<DebuggerMonoSystem, IDebuggerMonoSystem>(debuggerMonoSystem);
        }

        protected override void SetParentsActive()
        {
            stateMachinesParentTransform.gameObject.SetActive(true);
            monoSystemsParentTransform.gameObject.SetActive(true);
            controllersParentTransform.gameObject.SetActive(true);
        }
    }
}
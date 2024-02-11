using Akashic.Core;
using Akashic.Runtime.MonoSystems.Audio;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.GameDebug;
using Akashic.Runtime.StateMachines.GameStates;
using Akashic.Runtime.MonoSystems.Party;
using Akashic.Runtime.MonoSystems.PlayerPrefs;
using Akashic.Runtime.MonoSystems.Save;
using Akashic.Runtime.MonoSystems.Scene;
using Akashic.Runtime.MonoSystems.Story;
using Akashic.Runtime.MonoSystems.Inventory;
using Akashic.Runtime.StateMachines.BattleStates;
using Akashic.Runtime.StateMachines.ExplorationStates;
using Akashic.Runtime.StateMachines.StoryStates;
using UnityEngine;
using Akashic.Runtime.MonoSystems.Resource;

namespace Akashic.Runtime
{
    internal sealed class AkashicGameManager : GameManager
    {
        [Header("Management")] 
        [SerializeField] private Transform stateMachinesParentTransform;
        
        [SerializeField] private Transform monoSystemsParentTransform;
        
        [SerializeField] private Transform controllersParentTransform;
        
        [Header("StateMachines")]
        [SerializeField] private GameStateMachine gameStateMachine;
        [SerializeField] private BattleStateMachine battleStateMachine;
        [SerializeField] private ExplorationStateMachine explorationStateMachine;
        [SerializeField] private StoryStateMachine storyStateMachine;

        [Header("MonoSystems")]
        [SerializeField] private SceneMonoSystem sceneMonoSystem;
        [SerializeField] private AudioMonoSystem audioMonoSystem;
        [SerializeField] private PartyMonoSystem partyMonoSystem;
        [SerializeField] private ConfigMonoSystem configMonoSystem;
        [SerializeField] private PlayerPreferencesMonoSystem playerPreferencesMonoSystem;
        [SerializeField] private SaveMonoSystem saveMonoSystem;
        [SerializeField] private StoryMonoSystem storyMonoSystem;
		[SerializeField] private DebugMonoSystem debugMonoSystem;
		[SerializeField] private InventoryMonoSystem inventoryMonoSystem;
		[SerializeField] private ResourceMonoSystem resourceMonoSystem;

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
            AddStateMachine<GameStateMachine, IGameStateMachine>(gameStateMachine);
            AddStateMachine<BattleStateMachine, IBattleStateMachine>(battleStateMachine);
            AddStateMachine<ExplorationStateMachine, IExplorationStateMachine>(explorationStateMachine);
            AddStateMachine<StoryStateMachine, IStoryStateMachine>(storyStateMachine);
        }
        
        protected override void InitializeGameMonoSystems()
        {
            AddMonoSystem<SceneMonoSystem, ISceneMonoSystem>(sceneMonoSystem);
            AddMonoSystem<AudioMonoSystem, IAudioMonoSystem>(audioMonoSystem);
            AddMonoSystem<PartyMonoSystem, IPartyMonoSystem>(partyMonoSystem);
            AddMonoSystem<ConfigMonoSystem, IConfigMonoSystem>(configMonoSystem);
            AddMonoSystem<PlayerPreferencesMonoSystem, IPlayerPreferencesMonoSystem>(playerPreferencesMonoSystem);
            AddMonoSystem<SaveMonoSystem, ISaveMonoSystem>(saveMonoSystem);
            AddMonoSystem<StoryMonoSystem, IStoryMonoSystem>(storyMonoSystem);
			AddMonoSystem<DebugMonoSystem, IDebugMonoSystem>(debugMonoSystem);
			AddMonoSystem<InventoryMonoSystem, IInventoryMonoSystem>(inventoryMonoSystem);
			AddMonoSystem<ResourceMonoSystem, IResourceMonoSystem>(resourceMonoSystem);
		}

        protected override void SetParentsActive()
        {
            stateMachinesParentTransform.gameObject.SetActive(true);
            monoSystemsParentTransform.gameObject.SetActive(true);
            controllersParentTransform.gameObject.SetActive(true);
        }
    }
}
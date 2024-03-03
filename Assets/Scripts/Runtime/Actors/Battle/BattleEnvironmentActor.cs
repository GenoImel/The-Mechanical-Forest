using Akashic.Core;
using Akashic.Runtime.MonoSystems.GameDebug;
using Akashic.Runtime.StateMachines.BattleStates;
using Akashic.ScriptableObjects.Battle;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class BattleEnvironmentActor : MonoBehaviour
    {
        private BattleEnvironment currentBattleEnvironment;
        private EncounterData currentEncounterData;
        
        private Camera mainCamera;
        private IDebugMonoSystem debugMonoSystem;
        
        private IBattleStateMachine battleStateMachine;

        private void Awake()
        {
            mainCamera = Camera.main;

            debugMonoSystem = GameManager.GetMonoSystem<IDebugMonoSystem>();
            battleStateMachine = GameManager.GetStateMachine<IBattleStateMachine>();
        }
        
        private void Start()
        {
            if (!debugMonoSystem.IsDebugMode)
            {
                return;
            }
            
            var environmentToLoad = debugMonoSystem.GetDebugBattleEnvironment();
            
            currentEncounterData = debugMonoSystem.GetDebugEncounterData();
            LoadEnvironment(environmentToLoad);
        }

        private void OnEnable()
        {
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }

        private void LoadEnvironment(BattleEnvironment battleEnvironmentToLoad)
        {
            currentBattleEnvironment = Instantiate(battleEnvironmentToLoad, transform);
            InitializeEncounter();
        }

        private void InitializeEncounter()
        {
            currentBattleEnvironment.partyBattleActorInstantiator.InstantiatePartyBattleActors();
            currentBattleEnvironment.enemyBattleActorInstantiator
                .InitializeEnemyBattleActorsFromEncounterData(currentEncounterData);
            
            battleStateMachine.SetBattleActiveState();
        }
        
        private void UnloadEnvironment()
        {
            Destroy(currentBattleEnvironment.gameObject);
        }
        
        private void OnLoadBattleEnvironmentFromTriggerMessage(LoadBattleEnvironmentFromTriggerMessage message)
        {
            currentEncounterData = message.EncounterToLoad;
            LoadEnvironment(message.BattleEnvironmentToLoad);
        }
        
        private void AddListeners()
        {
            GameManager.AddListener<LoadBattleEnvironmentFromTriggerMessage>
                (OnLoadBattleEnvironmentFromTriggerMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.AddListener<LoadBattleEnvironmentFromTriggerMessage>
                (OnLoadBattleEnvironmentFromTriggerMessage);
        }
    }
}
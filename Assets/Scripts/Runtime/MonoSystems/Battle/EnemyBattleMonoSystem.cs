using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Enemy;
using Akashic.Runtime.MonoSystems.Timeline;
using Akashic.Runtime.ScriptableObjects.Battle;
using Akashic.Runtime.StateMachines.TurnStates;
using Akashic.Runtime.Utilities.GameMath.Resources;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Battle
{
    internal sealed class EnemyBattleMonoSystem : MonoBehaviour, IEnemyBattleMonoSystem
    {
        [Header("Battle Actors")]
        [SerializeField] private List<EnemyBattleActor> enemyBattleActors;
        
        private int currentAbilityPoints;
        private int maxAbilityPoints;
        
        public int CurrentAbilityPoints => currentAbilityPoints;
        
        private EncounterData encounterData;
        
        public EncounterData EncounterData => encounterData;

        private ITurnStateMachine turnStateMachine;

        private void Awake()
        {
            turnStateMachine = GameManager.GetStateMachine<ITurnStateMachine>();
        }

        private void OnEnable()
        {
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }

        public void SetEncounterData(EncounterData data)
        {
            encounterData = data;
        }

        public void AddEnemyBattleActor(EnemyBattleActor enemyBattleActor)
        {
            enemyBattleActors.Add(enemyBattleActor);
        }

        public void InitializeAbilityPoints()
        {
            maxAbilityPoints = ResourcesMath.CalculateTotalPooledAbilityPoints(enemyBattleActors);
            currentAbilityPoints = maxAbilityPoints;
        }
        
        public void SortEnemyBattleActorsBySpeed()
        {
            enemyBattleActors = enemyBattleActors
                .OrderByDescending(actor => actor.EnemyData.enemyClass.speedStat)
                .ToList();
        }
        
        public List<BattleActor> GetBattleActorsAsBase()
        {
            return enemyBattleActors.Cast<BattleActor>().ToList();
        }
        
        public List<EnemyBattleActor> GetBattleActors()
        {
            return enemyBattleActors.ToList();
        }
        
        private void OnTurnStateChangedMessage(TurnStateChangedMessage message)
        {
            if (message.NextState is not TurnFiniteState.EnemyPlanning)
            {
                return;
            }
            
            var enemyDecisionTasks = enemyBattleActors
                .Select(enemy => enemy.enemyBehaviour.ChooseActionAsync())
                .ToList();

            HandleEnemyDecisionsAsync(enemyDecisionTasks);
            
            GameManager.Publish(new EnemyMovesChosenMessage());
            turnStateMachine.SetPartyPlanningState();
        }

        private async void HandleEnemyDecisionsAsync(List<Task> enemyDecisionTasks)
        {
            await Task.WhenAll(enemyDecisionTasks);
        }
        
        private void AddListeners()
        {
            GameManager.AddListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
        }
    }
}
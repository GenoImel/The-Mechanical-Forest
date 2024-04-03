using System;
using System.Collections.Generic;
using System.Linq;
using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Enemy;
using Akashic.Runtime.StateMachines.TurnStates;
using Akashic.Runtime.Utilities.GameMath.Resources;
using Akashic.ScriptableObjects.Battle;
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
        
        private void OnTurnStateChangedMessage(TurnStateChangedMessage message)
        {
            if (message.NextState is TurnFiniteState.EnemyPlanning)
            {
                
            }
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
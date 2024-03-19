using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Actors.Battle;
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
            enemyBattleActors = enemyBattleActors.OrderByDescending(actor => actor.EnemyData.enemyClass.speedStat).ToList();
        }
    }
}
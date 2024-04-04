using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.MonoSystems.Battle;
using Akashic.Runtime.ScriptableObjects.Battle;
using Akashic.Runtime.Utilities.Random;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Akashic.Runtime.Actors.Battle.Environment
{
    internal sealed class EnemyBattleActorInstantiator : MonoBehaviour
    {
        [Header("Instantiation")]
        [SerializeField] private List<Transform> battleActorSpawnPoints;
        
        private IEnemyBattleMonoSystem enemyBattleMonoSystem;
        
        private void Awake()
        {
            enemyBattleMonoSystem = GameManager.GetMonoSystem<IEnemyBattleMonoSystem>();
        }

        public void InitializeEnemyBattleActorsFromEncounterData(EncounterData encounterData)
        {
            enemyBattleMonoSystem.SetEncounterData(encounterData);
            
            RandomizeEncounter(encounterData.enemiesAndQuantities);
        }

        private void RandomizeEncounter(List<EnemyQuantityRange> enemyQuantityRanges)
        {
            battleActorSpawnPoints = Shufflers.FisherYates(battleActorSpawnPoints);
            
            foreach (var enemyQuantityRange in enemyQuantityRanges)
            {
                var enemyQuantity = Random.Range(enemyQuantityRange.minQuantity, enemyQuantityRange.maxQuantity);

                for (var i = 0; i < enemyQuantity; i++)
                {
                    var enemyData = enemyQuantityRange.enemyData;
                    var instantiatedEnemyBattleActor = Instantiate(enemyData.enemyBattleActor, transform);
                    instantiatedEnemyBattleActor.transform.position = battleActorSpawnPoints[i].position;
                    
                    instantiatedEnemyBattleActor.InitializeEnemyBattleActor(enemyData);
                    enemyBattleMonoSystem.AddEnemyBattleActor(instantiatedEnemyBattleActor);
                }
            }
            
            enemyBattleMonoSystem.InitializeAbilityPoints();
            enemyBattleMonoSystem.SortEnemyBattleActorsBySpeed();
        }
    }
}
using System.Collections.Generic;
using Akashic.Runtime.Utilities.Random;
using Akashic.ScriptableObjects.Battle;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class EnemyBattleActorInstantiator : MonoBehaviour
    {
        [Header("Instantiation")]
        [SerializeField] private List<Transform> battleActorSpawnPoints;

        public void InitializeEnemyBattleActorsFromEncounterData(EncounterData encounterData)
        {
            battleActorSpawnPoints = Shufflers.FisherYates(battleActorSpawnPoints);
            RandomizeEncounter(encounterData.enemiesAndQuantities);
        }

        private void RandomizeEncounter(List<EnemyQuantityRange> enemyQuantityRanges)
        {
            foreach (var enemyQuantityRange in enemyQuantityRanges)
            {
                var enemyQuantity = Random.Range(enemyQuantityRange.minQuantity, enemyQuantityRange.maxQuantity);

                for (var i = 0; i < enemyQuantity; i++)
                {
                    var enemyData = enemyQuantityRange.enemyData;
                    var instantiatedEnemyBattleActor = Instantiate(enemyData.enemyBattleActor, transform);
                    instantiatedEnemyBattleActor.transform.position = battleActorSpawnPoints[i].position;
                }
            }
        }
    }
}
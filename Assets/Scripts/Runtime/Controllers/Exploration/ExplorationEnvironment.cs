using System;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.Runtime.Controllers.Exploration
{
    internal sealed class ExplorationEnvironment : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> playerSpawnPoints;

        public SpawnPoint GetPlayerSpawnPointById(string spawnPointId)
        {
            foreach (var spawnPoint in playerSpawnPoints)
            {
                if (spawnPoint.spawnPointId == spawnPointId)
                {
                    return spawnPoint;
                }
            }
            
            Debug.Log($"Spawn point {spawnPointId} not found. " +
                      $"Attempting to return DEFAULT.");
            
            foreach (var spawnPoint in playerSpawnPoints)
            {
                if (spawnPoint.spawnPointId == "DEFAULT")
                {
                    return spawnPoint;
                }
            }
            
            throw new Exception("No DEFAULT spawn point found. " +
                                "Please ensure there is always a DEFAULT spawn point defined.");
        }
    }
}
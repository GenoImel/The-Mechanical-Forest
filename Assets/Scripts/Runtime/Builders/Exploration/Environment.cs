using System;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.Runtime.Builders.Exploration
{
    internal sealed class Environment : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> playerSpawnPoints;
        [SerializeField] private List<SpawnPoint> cameraSpawnPoints;
        
        public SpawnPoint GetPlayerSpawnPointByName(string pointName)
        {
            foreach (var spawnPoint in playerSpawnPoints)
            {
                if (spawnPoint.spawnPointName == pointName)
                {
                    return spawnPoint;
                }
            }
            
            throw new Exception($"Spawn point ${pointName} not found.");
        }
        
        public SpawnPoint GetPlayerSpawnPointById(string spawnPointId)
        {
            foreach (var spawnPoint in playerSpawnPoints)
            {
                if (spawnPoint.spawnPointId == spawnPointId)
                {
                    return spawnPoint;
                }
            }
            
            throw new Exception($"Spawn point ${spawnPointId} not found.");
        }
        
        public SpawnPoint GetCameraSpawnPointByName(string pointName)
        {
            foreach (var spawnPoint in cameraSpawnPoints)
            {
                if (spawnPoint.spawnPointName == pointName)
                {
                    return spawnPoint;
                }
            }
            
            throw new Exception($"Spawn point ${pointName} not found.");
        }
        
        public SpawnPoint GetCameraSpawnPointById(string spawnPointId)
        {
            foreach (var spawnPoint in cameraSpawnPoints)
            {
                if (spawnPoint.spawnPointId == spawnPointId)
                {
                    return spawnPoint;
                }
            }
            
            throw new Exception($"Spawn point ${spawnPointId} not found.");
        }
    }
}
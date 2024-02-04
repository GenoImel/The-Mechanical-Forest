using System;
using Akashic.Core;
using Akashic.Runtime.Actors.Player;
using Akashic.Runtime.MonoSystems.GameDebug;
using Akashic.Runtime.MonoSystems.Scene;
using Akashic.ScriptableObjects.Exploration;
using UnityEngine;

namespace Akashic.Runtime.Controllers.Exploration
{
    internal sealed class EnvironmentController : MonoBehaviour
    {
        [SerializeField] private PlayerActor playerActor;
        [SerializeField] private Camera mainCamera;
        
        private ExplorationEnvironment currentExplorationEnvironment;
        private ExplorationEnvironmentData currentExplorationEnvironmentData;

        private IDebugMonoSystem debugMonoSystem;
        
        private void Awake()
        {
            mainCamera = Camera.main;
            
            debugMonoSystem = GameManager.GetMonoSystem<IDebugMonoSystem>();
        }

        private void Start()
        {
            if (!debugMonoSystem.IsDebugMode)
            {
                return;
            }
            
            var environmentToLoad = debugMonoSystem.GetDebugEnvironment();
            
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
        
        private void LoadEnvironment(ExplorationEnvironmentData environmentToLoad, string spawnPointId = "DEFAULT")
        {
            currentExplorationEnvironmentData = environmentToLoad;
            currentExplorationEnvironment = Instantiate(environmentToLoad.explorationEnvironment);
            
            SpawnPlayerAtSpawnPoint(spawnPointId);
        }

        private void UnloadEnvironment()
        {
            Destroy(currentExplorationEnvironment.gameObject);
        }
        
        private void SpawnPlayerAtSpawnPoint(string spawnPointId)
        {
            var playerSpawnPoint = currentExplorationEnvironment.GetPlayerSpawnPointById(spawnPointId);
            
            if (playerSpawnPoint == null)
            {
                throw new Exception($"Spawn point with ID {spawnPointId} not found.");
            }
            playerActor.transform.position = playerSpawnPoint.transform.position;

            if (playerSpawnPoint.initialCameraPlacement == null)
            {
                throw new Exception($"Initial camera position associated with ID " +
                                    $"{spawnPointId} could not be found.");
            }

            var initialCameraPlacement = playerSpawnPoint.initialCameraPlacement;
            var mainCameraTransform = mainCamera.transform;
            
            mainCameraTransform.position = initialCameraPlacement.position;
            mainCameraTransform.rotation = initialCameraPlacement.rotation;
        }

        private void OnSceneInitializationStartedMessage(StartSceneInitializationMessage message)
        {
            // Do some stuff for scene initialization here.
            GameManager.Publish(new SceneInitializedMessage());
        }

        private void OnLoadExplorationEnvironmentMessage(LoadExplorationEnvironmentMessage message)
        {
            Destroy(currentExplorationEnvironment.gameObject);
            
            var environmentToLoad = message.ExplorationEnvironmentToLoad;
            
            LoadEnvironment(environmentToLoad);
        }

        private void OnLoadExplorationEnvironmentFromTriggerMessage(
            LoadExplorationEnvironmentFromTriggerMessage message
            )
        {
            var spawnPointId = $"Spawn_{currentExplorationEnvironmentData.roomId}_{message.TriggerId}";
            Debug.Log($"Loading new environment with {spawnPointId}");
            
            UnloadEnvironment();
            LoadEnvironment(message.ExplorationEnvironmentToLoad, spawnPointId);
        }

        private void AddListeners()
        {
            GameManager.AddListener<StartSceneInitializationMessage>(OnSceneInitializationStartedMessage);
            GameManager.AddListener<LoadExplorationEnvironmentMessage>(OnLoadExplorationEnvironmentMessage);
            GameManager.AddListener<LoadExplorationEnvironmentFromTriggerMessage>
                (OnLoadExplorationEnvironmentFromTriggerMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<StartSceneInitializationMessage>(OnSceneInitializationStartedMessage);
            GameManager.RemoveListener<LoadExplorationEnvironmentMessage>(OnLoadExplorationEnvironmentMessage);
            GameManager.AddListener<LoadExplorationEnvironmentFromTriggerMessage>
                (OnLoadExplorationEnvironmentFromTriggerMessage);
        }
    }
}
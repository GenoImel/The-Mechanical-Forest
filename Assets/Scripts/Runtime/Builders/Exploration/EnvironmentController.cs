using System;
using Akashic.Core;
using Akashic.Runtime.Actors.Player;
using Akashic.Runtime.MonoSystems.GameDebug;
using Akashic.Runtime.MonoSystems.Scene;
using Akashic.ScriptableObjects.Exploration;
using UnityEngine;

namespace Akashic.Runtime.Builders.Exploration
{
    internal sealed class EnvironmentController : MonoBehaviour
    {
        [SerializeField] private PlayerActor playerActor;
        [SerializeField] private Camera mainCamera;
        
        private Environment currentExplorationScene;

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
        
        private void LoadEnvironment(ExplorationEnvironmentData environmentToLoad)
        {
            currentExplorationScene = Instantiate(environmentToLoad.environment);
            
            SpawnPlayerAtSpawnPoint("default");
            MoveMainCameraToSpawnPoint("default");
        }
        
        private void SpawnPlayerAtSpawnPoint(string spawnPointName)
        {
            var defaultPlayerSpawnPoint = currentExplorationScene.GetPlayerSpawnPointByName(spawnPointName);
            
            if (defaultPlayerSpawnPoint == null)
            {
                throw new Exception("Default spawn point not found.");
            }
            playerActor.transform.position = defaultPlayerSpawnPoint.transform.position;
        }
        
        private void MoveMainCameraToSpawnPoint(string spawnPointName)
        {
            var cameraSpawnPoint = currentExplorationScene.GetCameraSpawnPointByName(spawnPointName);
            
            if (cameraSpawnPoint == null)
            {
                throw new Exception("Default spawn point not found.");
            }

            var cameraTransform = cameraSpawnPoint.transform;
            var mainCameraTransform = mainCamera.transform;
            
            mainCameraTransform.position = cameraTransform.position;
            mainCameraTransform.rotation = cameraTransform.rotation;
        }

        private void OnSceneInitializationStartedMessage(StartSceneInitializationMessage message)
        {
            // Do some stuff for scene initialization here.
            GameManager.Publish(new SceneInitializedMessage());
        }

        private void OnLoadNewExplorationSceneMessage(LoadNewExplorationSceneMessage message)
        {
            Destroy(currentExplorationScene.gameObject);
            
            var environmentToLoad = message.ExplorationEnvironmentToLoad;
            
            LoadEnvironment(environmentToLoad);
        }

        private void AddListeners()
        {
            GameManager.AddListener<StartSceneInitializationMessage>(OnSceneInitializationStartedMessage);
            GameManager.AddListener<LoadNewExplorationSceneMessage>(OnLoadNewExplorationSceneMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<StartSceneInitializationMessage>(OnSceneInitializationStartedMessage);
            GameManager.RemoveListener<LoadNewExplorationSceneMessage>(OnLoadNewExplorationSceneMessage);
        }
    }
}
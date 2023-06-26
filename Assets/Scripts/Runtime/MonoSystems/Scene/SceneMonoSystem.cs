using System;
using System.Collections;
using NaughtyAttributes;
using Akashic.Core;
using Akashic.Runtime.Controllers.LoadingCurtain;
using Akashic.Runtime.MonoSystems.GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Akashic.Runtime.MonoSystems.Scene
{
    internal sealed class SceneMonoSystem : MonoBehaviour, ISceneMonoSystem
    {
        [Header("Scenes")] 
        [Scene]
        [SerializeField] private int mainMenuSceneBuildIndex;

        [Scene]
        [SerializeField] private int explorationSceneBuildIndex;

        [Scene]
        [SerializeField] private int battleSceneBuildIndex;

        [Header("Curtains")] 
        [SerializeField] private LoadingCurtainController loadingCurtainController;
        
        public bool IsSceneLoading { get; private set; }
        
        public bool IsSceneInitialized { get; private set; }

        private IGameStateMonoSystem gameStateMonoSystem;

        private void Awake()
        {
            gameStateMonoSystem = GameManager.GetMonoSystem<IGameStateMonoSystem>();
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        public void LoadMainMenuScene()
        {
            BeginLoadingScene(mainMenuSceneBuildIndex, MainMenuSceneLoaded, true);
        }

        public void LoadExplorationScene()
        {
            BeginLoadingScene(explorationSceneBuildIndex, ExplorationSceneLoaded);
        }

        public void LoadBattleScene()
        {
            BeginLoadingScene(battleSceneBuildIndex, BattleSceneLoaded);
        }

        private void BeginLoadingScene(
            int index, 
            Action onLoaded, 
            bool isPreInitialized = false
            )
        {
            StartCoroutine(LoadSceneRoutine(index, onLoaded));
        }

        private IEnumerator LoadSceneRoutine(
            int index, 
            Action onLoaded, 
            bool isPreInitialized = false
            )
        {
            if (IsSceneLoading)
            {
                yield break;
            }

            IsSceneLoading = true;
            IsSceneInitialized = isPreInitialized;

            yield return loadingCurtainController.ShowCurtain();
            yield return new WaitForSeconds(5f);
            yield return SceneManager.LoadSceneAsync(index);
            yield return IsSceneInitialized;
            yield return loadingCurtainController.HideCurtain();

            onLoaded?.Invoke();

            IsSceneLoading = false;
        }

        private void MainMenuSceneLoaded()
        {
            gameStateMonoSystem.SetMainMenuState();
        }

        private void ExplorationSceneLoaded()
        {
            gameStateMonoSystem.SetExplorationState();
        }

        private void BattleSceneLoaded()
        {
            gameStateMonoSystem.SetBattleState();
        }

        private void OnSceneInitializedMessage(SceneInitializedMessage message)
        {
            IsSceneInitialized = true;
        }

        private void AddListeners()
        {
            GameManager.AddListener<SceneInitializedMessage>(OnSceneInitializedMessage);
        }

        private void RemoveListeners()
        {
            GameManager.RemoveListener<SceneInitializedMessage>(OnSceneInitializedMessage);
        }
    }
}

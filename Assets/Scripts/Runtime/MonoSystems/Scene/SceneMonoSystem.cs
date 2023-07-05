using System;
using NaughtyAttributes;
using Akashic.Core;
using Akashic.Runtime.Controllers.LoadingCurtain;
using Akashic.Runtime.MonoSystems.GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


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
        [SerializeField] private BattleCurtainController battleCurtainController;

        private bool isSceneLoading;

        private bool isSceneInitialized;

        private bool sceneInitializationStarted;

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
            LoadSceneAsync(mainMenuSceneBuildIndex, loadingCurtainController, MainMenuSceneLoaded, true);
        }

        public void LoadExplorationScene()
        {
            LoadSceneAsync(explorationSceneBuildIndex, loadingCurtainController, ExplorationSceneLoaded);
        }

        public void LoadBattleScene()
        {
            LoadSceneAsync(battleSceneBuildIndex, battleCurtainController, BattleSceneLoaded);
        }

        private async void LoadSceneAsync(
            int index, 
            CurtainController curtain, 
            Action onLoaded, 
            bool isPreInitialized = false
            )
        {
            if (isSceneLoading)
            {
                return;
            }

            isSceneLoading = true;
            isSceneInitialized = isPreInitialized;
            sceneInitializationStarted = isPreInitialized;

            await curtain.ShowCurtain();
            SceneManager.LoadSceneAsync(index);
            await Task.Delay(1000);

            if (!isSceneInitialized && !sceneInitializationStarted)
            {
                GameManager.Publish(new StartSceneInitializationMessage());
                sceneInitializationStarted = true;
            }

            if (!isSceneInitialized)
            {
                await Task.Yield();
            }

            await curtain.HideCurtain();

            onLoaded?.Invoke();

            isSceneLoading = false;
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
            isSceneInitialized = true;
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

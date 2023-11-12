using System;
using NaughtyAttributes;
using Akashic.Core;
using Akashic.Runtime.Controllers.LoadingCurtain;
using Akashic.Runtime.StateMachines.GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Akashic.Runtime.Common;
using Akashic.Runtime.Controllers.BattleCurtain;


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

        private IGameStateMachine gameStateMachine;

        private void Awake()
        {
            gameStateMachine = GameManager.GetStateMachine<IGameStateMachine>();
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
            gameStateMachine.SetMainMenuState();
        }

        private void ExplorationSceneLoaded()
        {
            gameStateMachine.SetExplorationState();
        }

        private void BattleSceneLoaded()
        {
            gameStateMachine.SetBattleState();
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

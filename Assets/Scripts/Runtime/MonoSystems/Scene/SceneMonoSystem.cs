using System;
using System.Collections;
using NaughtyAttributes;
using Akashic.Core;
using Akashic.Runtime.Controllers.LoadingCurtain;
using Akashic.Runtime.MonoSystems.GameStates;
using Akashic.Runtime.Utilities;
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
        [SerializeField] private CanvasGroup loadingCanvasGroup;
        [SerializeField] private CanvasGroup battleCanvasGroup;

        [Header("Settings")] 
        [SerializeField] private float canvasFadeDurationSeconds;
        
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
            LoadSceneRoutine(mainMenuSceneBuildIndex, loadingCanvasGroup, MainMenuSceneLoaded, true);
        }

        public void LoadExplorationScene()
        {
            LoadSceneRoutine(explorationSceneBuildIndex, loadingCanvasGroup, ExplorationSceneLoaded);
        }

        public void LoadBattleScene()
        {
            LoadSceneRoutine(battleSceneBuildIndex, battleCanvasGroup, BattleSceneLoaded);
        }

        private async void LoadSceneRoutine(
            int index, 
            CanvasGroup canvasGroup, 
            Action onLoaded, 
            bool isPreInitialized = false
            )
        {
            if (IsSceneLoading)
            {
                return;
            }

            IsSceneLoading = true;
            IsSceneInitialized = isPreInitialized;

            await CanvasUtilities.ShowCurtain(canvasGroup, canvasFadeDurationSeconds);
            await Task.Delay(5000);
            SceneManager.LoadSceneAsync(index);

            if (!IsSceneInitialized)
            {
              return;
            }
            
            await CanvasUtilities.HideCurtain(canvasGroup, canvasFadeDurationSeconds);

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

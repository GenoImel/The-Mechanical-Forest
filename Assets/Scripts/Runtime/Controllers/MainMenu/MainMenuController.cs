using System;
using Akashic.Core;
using Akashic.Runtime.Controllers.OptionsMenu;
using Akashic.Runtime.Controllers.SaveMenu;
using Akashic.Runtime.MonoSystems.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button quitButton;
        
        [Header("Main Theme")]
        [SerializeField] private AudioClip mainTheme;

        private ISoundMonoSystem soundMonoSystem;

        private void Awake()
        {
            soundMonoSystem = GameManager.GetMonoSystem<ISoundMonoSystem>();
        }

        private void Start()
        {
            soundMonoSystem.PlayMusic(mainTheme);
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        private void OnNewGameButtonClicked()
        {
            GameManager.Publish(new ShowSaveMenuMessage());
        }

        private void OnOptionsButtonClicked()
        {
            GameManager.Publish(new ShowOptionsMenuMessage());
        }

        private void OnQuitButtonClicked()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
        private void AddListeners()
        {
            newGameButton.onClick.AddListener(OnNewGameButtonClicked);
            optionsButton.onClick.AddListener(OnOptionsButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
        
        private void RemoveListeners()
        {
            newGameButton.onClick.RemoveListener(OnNewGameButtonClicked);
            optionsButton.onClick.RemoveListener(OnOptionsButtonClicked);
            quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }
    }
}
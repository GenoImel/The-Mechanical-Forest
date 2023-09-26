using UnityEngine;
using UnityEngine.UI;
using Akashic.Core;
using Akashic.Runtime.Common;
using Akashic.Runtime.MonoSystems.GameStates;

namespace Akashic.Runtime.Controllers.SaveMenu
{
    internal sealed class SaveMenuController : OverlayController
    {
        [Header("UI elements")]
        [SerializeField] private Button backButton;

        [Header("Panels")]
        [SerializeField] private SaveFileContainer saveFileContainer;
        
        [Header("Panels")]
        [SerializeField] private NewFileNameRequestPanel newFileNameRequestPanel;

        private void Start()
        {
            Hide();
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        private void OnBackButtonPressed()
        {
            newFileNameRequestPanel.Hide();
            Hide();
        }

        private void OnShowSaveMenuMessage(ShowSaveMenuMessage message)
        {
            saveFileContainer.FindSaveFiles();
            Show();
        }

        private void OnHideSaveMenuMessage(HideSaveMenuMessage message)
        {
            Hide();
        }

        private void OnGameStateChangedMessage(GameStateChangedMessage message)
        {
            Hide();
        }

        private void AddListeners()
        {
            backButton.onClick.AddListener(OnBackButtonPressed);
            
            GameManager.AddListener<ShowSaveMenuMessage>(OnShowSaveMenuMessage);
            GameManager.AddListener<HideSaveMenuMessage>(OnHideSaveMenuMessage);
            GameManager.AddListener<GameStateChangedMessage>(OnGameStateChangedMessage);
        }

        private void RemoveListeners()
        {
            backButton.onClick.RemoveListener(OnBackButtonPressed);
            
            GameManager.RemoveListener<ShowSaveMenuMessage>(OnShowSaveMenuMessage);
            GameManager.RemoveListener<HideSaveMenuMessage>(OnHideSaveMenuMessage);
            GameManager.RemoveListener<GameStateChangedMessage>(OnGameStateChangedMessage);
        }
    }
}
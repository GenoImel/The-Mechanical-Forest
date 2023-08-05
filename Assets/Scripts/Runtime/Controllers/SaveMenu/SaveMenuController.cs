using Akashic.Core;
using Akashic.Runtime.MonoSystems.GameStates;
using Akashic.Runtime.Utilities.Canvas;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.SaveMenu
{
    internal sealed class SaveMenuController : MonoBehaviour
    {
        [Header("UI elements")]
        [SerializeField] private Button backButton;

        [SerializeField] private CanvasGroup canvasGroup;

        private void Start()
        {
            Hide();
        }

        private void Hide()
        {
            CanvasUtilities.HideCanvas(canvasGroup);
        }

        private void Show()
        {
            CanvasUtilities.ShowCanvas(canvasGroup);
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
            Hide();
        }

        private void OnShowSaveMenuMessage(ShowSaveMenuMessage message)
        {
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
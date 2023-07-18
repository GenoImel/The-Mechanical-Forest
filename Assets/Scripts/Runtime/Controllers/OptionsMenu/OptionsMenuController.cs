using Akashic.Core;
using Akashic.Runtime.Utilities.Canvas;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.OptionsMenu
{
    internal sealed class SettingsMenuController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button backButton;
        
        [Header("UI")]
        [SerializeField] private CanvasGroup canvasGroup;
        
        private void OnEnable()
        {
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }
        
        private void Hide()
        {
            CanvasUtilities.HideCanvas(canvasGroup);
        }
        
        private void Show()
        {
            CanvasUtilities.ShowCanvas(canvasGroup);
        }
        
        private void OnBackButtonClicked()
        {
            GameManager.Publish(new OptionsMenuClosedMessage());
            Hide();
        }
        
        private void OnShowSettingsMenuMessage(ShowOptionsMenuMessage message)
        {
            Show();
        }
        
        private void AddListeners()
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
            
            GameManager.AddListener<ShowOptionsMenuMessage>(OnShowSettingsMenuMessage);
        }
        
        private void RemoveListeners()
        {
            backButton.onClick.RemoveListener(OnBackButtonClicked);
            
            GameManager.RemoveListener<ShowOptionsMenuMessage>(OnShowSettingsMenuMessage);
        }
    }
}
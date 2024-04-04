using Akashic.Core;
using Akashic.Runtime.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.OptionsMenu
{
    internal sealed class OptionsMenuController : OverlayController
    {
        [Header("Buttons")]
        [SerializeField] private Button backButton;
        
        private void OnEnable()
        {
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
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
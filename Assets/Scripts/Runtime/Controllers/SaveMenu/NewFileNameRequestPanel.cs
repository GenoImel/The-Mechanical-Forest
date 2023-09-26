using System;
using Akashic.Core;
using Akashic.Runtime.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.SaveMenu
{
    internal sealed class NewFileNameRequestPanel : OverlayController
    {
        [Header("Buttons")]
        [SerializeField] private Button cancelButton;
        
        [SerializeField] private Button confirmButton;
        
        [Header("Input fields")]
        [SerializeField] private TMP_Text fileNameInputText;
        
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
        
        private void OnCancelButtonClicked()
        {
            Hide();
        }
        
        private void OnConfirmButtonClicked()
        {
            var fileName = fileNameInputText.text;
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("File name is empty.");
            }
            
            GameManager.Publish(new NewFileNameConfirmedMessage(fileName));
            
            Hide();
        }

        private void OnNewFileNameRequestedMessage(RequestNewFileNameMessage message)
        {
            Show();
        }

        private void AddListeners()
        {
            cancelButton.onClick.AddListener(OnCancelButtonClicked);
            confirmButton.onClick.AddListener(OnConfirmButtonClicked);
            
            GameManager.AddListener<RequestNewFileNameMessage>(OnNewFileNameRequestedMessage);
        }

        private void RemoveListeners()
        {
            cancelButton.onClick.RemoveListener(OnCancelButtonClicked);
            confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
            
            GameManager.RemoveListener<RequestNewFileNameMessage>(OnNewFileNameRequestedMessage);
        }
    }
}
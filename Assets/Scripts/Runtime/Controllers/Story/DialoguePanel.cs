using Akashic.Core;
using Akashic.Runtime.Common;
using Akashic.Runtime.MonoSystems.Story;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class DialoguePanel : OverlayController, IPointerClickHandler
    {
        [Header("Text Elements")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;

        [Header("Buttons")]
        [SerializeField] private Button logButton;

        public event EventHandler onDialoguePanelClickedEvent;
        public UnityEvent onLogButtonClickedEvent;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        public override void Hide()
        {
            base.Hide();
            SetCharacterName(null);
            SetDialogue(null);
        }

        public void DisplayDialogueLine(StoryPoint storyPoint)
        {
            SetCharacterName(storyPoint.characterName);
            SetDialogue(storyPoint.dialogueLine);
        }

        private void SetCharacterName(string name)
        {
            nameText.text = name;
        }

        private void SetDialogue(string dialogue)
        {
            dialogueText.text = dialogue;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onDialoguePanelClickedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void OnLogButtonClicked()
        {
            onLogButtonClickedEvent?.Invoke();
        }

        private void OnToggleEventLog(ToggleEventLog message)
        {
            logButton.gameObject.SetActive(message.allowLog);
        }

        /// <summary>
        /// Ensures that the DialoguePanel cannot be clicked when the StoryEventLog is open.
        /// </summary>
        /// <param name="message"></param>
        private void OnStoryEventLogOpened(StoryEventLogOpened message)
        {
            canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// Allows the DialoguePanel to be clicked.
        /// </summary>
        /// <param name="message"></param>
        private void OnStoryEventLogClosed(StoryEventLogClosed message)
        {
            canvasGroup.blocksRaycasts = true;
        }

        private void AddListeners()
        {
            logButton.onClick.AddListener(OnLogButtonClicked);
            GameManager.AddListener<ToggleEventLog>(OnToggleEventLog);
            GameManager.AddListener<StoryEventLogOpened>(OnStoryEventLogOpened);
            GameManager.AddListener<StoryEventLogClosed>(OnStoryEventLogClosed);
        }

        private void RemoveListeners()
        {
            logButton.onClick.RemoveListener(OnLogButtonClicked);
            GameManager.RemoveListener<ToggleEventLog>(OnToggleEventLog);
            GameManager.RemoveListener<StoryEventLogOpened>(OnStoryEventLogOpened);
            GameManager.RemoveListener<StoryEventLogClosed>(OnStoryEventLogClosed);
        }
    }
}

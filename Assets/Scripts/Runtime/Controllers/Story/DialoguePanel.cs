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

        private void AddListeners()
        {
            logButton.onClick.AddListener(OnLogButtonClicked);
        }

        private void RemoveListeners()
        {
            logButton.onClick.RemoveListener(OnLogButtonClicked);
        }
    }
}

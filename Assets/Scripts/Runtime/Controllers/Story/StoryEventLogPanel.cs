using System.Collections.Generic;
using UnityEngine;
using Akashic.Runtime.Common;
using Akashic.Runtime.MonoSystems.Story;
using UnityEngine.UI;
using Akashic.Core;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class StoryEventLogPanel : OverlayController
    {
        [Header("Buttons")]
        [SerializeField] private Button backButton;

        [Header("Prefab")]
        [SerializeField] private DialogueEntry dialogueEntryPrefab;
        [SerializeField] private GameObject dialogueEntryParent;

        private List<DialogueEntry> dialogueEntries = new List<DialogueEntry>();

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void Start()
        {
            Hide();
        }

        public void ShowStoryEventLog()
        {
            Show();
        }

        public void AddDialogueEntry(DialogueEntryAvailableMessage message)
        {
            DialogueEntry newDialogueEntry = Instantiate(dialogueEntryPrefab, dialogueEntryParent.transform);
            newDialogueEntry.name = $"DialogueEntry{dialogueEntries.Count}";
            newDialogueEntry.SetText($"{message.StoryPoint.characterName}:", message.StoryPoint.dialogueLine);
            dialogueEntries.Add(newDialogueEntry);
        }

        public void DestroyDialogueEntries()
        {
            foreach (DialogueEntry entry in dialogueEntries)
            {
                Destroy(entry.gameObject);
            }
            dialogueEntries.Clear();
        }

        private void OnBackButtonClicked()
        {
            Hide();
        }

        private void AddListeners()
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
            GameManager.AddListener<DialogueEntryAvailableMessage>(AddDialogueEntry);
        }

        private void RemoveListeners()
        {
            backButton.onClick.RemoveListener(OnBackButtonClicked);
        }
    }
}

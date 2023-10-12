using System.Collections.Generic;
using UnityEngine;
using Akashic.Runtime.Common;
using Akashic.Runtime.MonoSystems.Story;
using UnityEngine.UI;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class StoryEventLogPanel : OverlayController
    {
        [Header("Buttons")]
        [SerializeField] private Button backButton;

        [Header("Prefab")]
        [SerializeField] private DialogueEntry dialogueEntryPrefab;

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

        public void AddNewLine(StoryPoint storyPoint)
        {

        }

        private void OnBackButtonClicked()
        {
            Hide();
        }

        private void AddListeners()
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void RemoveListeners()
        {
            backButton.onClick.RemoveListener(OnBackButtonClicked);
        }
    }
}

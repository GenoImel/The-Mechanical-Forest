using Akashic.Core;
using UnityEngine;
using Akashic.Runtime.Common;
using Akashic.Runtime.MonoSystems.Story;
using System;
using Akashic.Runtime.StateMachines.StoryStates;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class StoryController : OverlayController
    {
        [Header("Panels")]
        [SerializeField] private DialoguePanel dialoguePanel;
        [SerializeField] private StoryEventLogPanel storyEventLogPanel;

        private StoryPoint currentStoryPoint;

        private IStoryMonoSystem storyMonoSystem;
        private IStoryStateMachine storyState;

        private void Awake()
        {
            storyMonoSystem = GameManager.GetMonoSystem<IStoryMonoSystem>();
            storyState = GameManager.GetStateMachine<IStoryStateMachine>();
        }

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
            dialoguePanel.Hide();
            Hide();
        }
        
        private void ShowStoryPointDialogue(StoryPoint storyPoint)
        {
            dialoguePanel.DisplayDialogueLine(storyPoint);
            Show();
            dialoguePanel.Show();
        }

        public void ShowStoryEventLog()
        {
            storyEventLogPanel.ShowStoryEventLog();
        }

        private void OnStoryPointAvailableMessage(StoryPointAvailableMessage message)
        {
            currentStoryPoint = storyMonoSystem.GetCurrentStoryPoint();
            ShowStoryPointDialogue(currentStoryPoint);
        }

        private void OnStoryEventEndedMessage(StoryEventEndedMessage message)
        {
            Hide();
            dialoguePanel.Hide();
            storyEventLogPanel.DestroyDialogueEntries();
            storyState.SetInactiveState();
        }

        private void ProgressDialogue(object sender, EventArgs e)
        {
            storyMonoSystem.AdvanceStoryPoint();
        }

        private void AddListeners()
        {
            GameManager.AddListener<StoryPointAvailableMessage>(OnStoryPointAvailableMessage);
            GameManager.AddListener<StoryEventEndedMessage>(OnStoryEventEndedMessage);
            dialoguePanel.onDialoguePanelClickedEvent += ProgressDialogue;
            dialoguePanel.onLogButtonClickedEvent.AddListener(ShowStoryEventLog);
        }

        private void RemoveListeners()
        {
            GameManager.RemoveListener<StoryPointAvailableMessage>(OnStoryPointAvailableMessage);
            GameManager.RemoveListener<StoryEventEndedMessage>(OnStoryEventEndedMessage);
            dialoguePanel.onDialoguePanelClickedEvent -= ProgressDialogue;
            dialoguePanel.onLogButtonClickedEvent.RemoveListener(ShowStoryEventLog);
        }
    }
}

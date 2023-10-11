using Akashic.Core;
using UnityEngine;
using Akashic.Runtime.Common;
using Akashic.Runtime.MonoSystems.Story;
using System;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class StoryController : OverlayController
    {
        [Header("Panels")]
        [SerializeField] private DialoguePanel dialoguePanel;

        private StoryPoint currentStoryPoint;

        private IStoryMonoSystem storyMonoSystem;

        private void Awake()
        {
            storyMonoSystem = GameManager.GetMonoSystem<IStoryMonoSystem>();
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
        
        private void OnStoryEventAvailableMessage(StoryEventAvailableMessage message)
        {
            currentStoryPoint = storyMonoSystem.GetCurrentStoryPoint();
            ShowStoryPointDialogue(currentStoryPoint);
        }

        private void OnStoryEventEndedMessage(StoryEventEndedMessage message)
        {
            Hide();
            dialoguePanel.Hide();
        }

        private void ProgressDialogue(object sender, EventArgs e)
        {
            storyMonoSystem.AdvanceStoryPoint();
        }

        private void AddListeners()
        {
            GameManager.AddListener<StoryEventAvailableMessage>(OnStoryEventAvailableMessage);
            GameManager.AddListener<StoryEventEndedMessage>(OnStoryEventEndedMessage);
            dialoguePanel.OnDialoguePanelClicked += ProgressDialogue;
        }

        private void RemoveListeners()
        {
            GameManager.RemoveListener<StoryEventAvailableMessage>(OnStoryEventAvailableMessage);
            GameManager.RemoveListener<StoryEventEndedMessage>(OnStoryEventEndedMessage);
            dialoguePanel.OnDialoguePanelClicked -= ProgressDialogue;
        }
    }
}

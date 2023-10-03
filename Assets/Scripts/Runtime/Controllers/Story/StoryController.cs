using UnityEngine;
using TMPro;
using Akashic.Runtime.Common;
using Akashic.Runtime.MonoSystems.Story;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class StoryController : OverlayController
    {
        [Header("Panels")]
        [SerializeField] private DialoguePanel dialoguePanel;

        private void Start()
        {
            dialoguePanel.Hide();
            Hide();
        }

        public void ShowStoryPointDialogue(StoryPoint storyPoint)
        {
            dialoguePanel.SetCharacterName(storyPoint.characterName);
            dialoguePanel.SetDialogue(storyPoint.dialogueLine);
            Show();
            dialoguePanel.Show();
        }
    }
}

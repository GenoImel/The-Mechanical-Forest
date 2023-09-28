using UnityEngine;
using TMPro;
using Akashic.Runtime.Common;
using Akashic.Runtime.MonoSystems.Story;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class StoryController : OverlayController
    {
        [Header("UI elements")]
        [SerializeField] private DialoguePanel dialoguePanel;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;

        private void Start()
        {
            dialoguePanel.Hide();
            Hide();
        }

        public void ShowStoryPointDialogue(StoryPoint storyPoint)
        {
            nameText.text = storyPoint.characterName;
            dialogueText.text = storyPoint.dialogueLine;
            Show();
            dialoguePanel.Show();
        }
    }
}

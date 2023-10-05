using Akashic.Runtime.Common;
using Akashic.Runtime.MonoSystems.Story;
using TMPro;
using UnityEngine;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class DialoguePanel : OverlayController
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;

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
    }
}

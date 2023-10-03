using Akashic.Runtime.Common;
using TMPro;
using UnityEngine;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class DialoguePanel : OverlayController
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;

        public void SetCharacterName(string name)
        {
            nameText.text = name;
        }

        public void SetDialogue(string dialogue)
        {
            dialogueText.text = dialogue;
        }

    }
}

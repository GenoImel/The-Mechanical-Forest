using TMPro;
using UnityEngine;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class DialogueEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI characterNameText;
        [SerializeField] private TextMeshProUGUI characterDialogueText;

        public void SetText(string characterName, string characterDialogue)
        {
            characterNameText.text = characterName;
            characterDialogueText.text = characterDialogue;
        }
    }
}

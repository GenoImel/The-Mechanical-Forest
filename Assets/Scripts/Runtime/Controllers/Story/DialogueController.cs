using UnityEngine;
using TMPro;
using Akashic.Runtime.Utilities.Canvas;
using Akashic.Runtime.MonoSystems.Story;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class DialogueController : MonoBehaviour
    {
        [Header("UI elements")]
        [SerializeField] private CanvasGroup dialogueCanvas;
        [SerializeField] private CanvasGroup dialoguePanel;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;

        private void Start()
        {
            Hide();
        }

        private void Hide()
        {
            CanvasUtilities.HideCanvas(dialogueCanvas);
            CanvasUtilities.HideCanvas(dialoguePanel);
        }

        private void Show()
        {
            CanvasUtilities.ShowCanvas(dialogueCanvas);
            CanvasUtilities.ShowCanvas(dialoguePanel);
        }

        public void ShowStoryPointDialogue(StoryPoint storyPoint)
        {
            nameText.text = storyPoint.characterName;
            dialogueText.text = storyPoint.dialogueLine;
            Show();
        }
    }
}

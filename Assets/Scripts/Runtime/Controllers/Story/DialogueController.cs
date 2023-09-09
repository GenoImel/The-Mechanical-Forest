using UnityEngine;
using TMPro;
using Akashic.Runtime.Utilities.Canvas;
using Akashic.Runtime.MonoSystems.Story;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class DialogueController : MonoBehaviour
    {
        /*
         * Hide and show private methods
         * public method for passing dialogue accept the story event SO
         */
        [Header("UI elements")]
        [SerializeField] private CanvasGroup dialogueCanvas;
        [SerializeField] private CanvasGroup dialoguePanel;
        [SerializeField] private TextMeshProUGUI dialogueText;

        private void Start()
        {
            Hide();
        }

        private void Hide()
        {
            CanvasUtilities.HideCanvas(dialogueCanvas);
        }

        private void Show()
        {
            CanvasUtilities.ShowCanvas(dialogueCanvas);
            CanvasUtilities.ShowCanvas(dialoguePanel);
        }

        public void ShowStoryPointDialogue(StoryPoint storyPoint)
        {
            dialogueText.text = storyPoint.dialogueLine;
            Show();
        }
    }
}

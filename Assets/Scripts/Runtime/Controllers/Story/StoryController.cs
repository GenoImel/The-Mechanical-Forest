using UnityEngine;
using TMPro;
using Akashic.Runtime.Common;
using Akashic.Runtime.Utilities.Canvas;
using Akashic.Runtime.MonoSystems.Story;

namespace Akashic.Runtime.Controllers.Story
{
    internal sealed class StoryController : OverlayController
    {
        [Header("UI elements")]
        // The dialogue panel will eventually have it's own script.
        [SerializeField] private CanvasGroup dialoguePanel;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;

        private void Start()
        {
            Hide();
        }

        // Overriding inherited OverlayController methods until DialoguePanel has its own script.
        private new void Hide()
        {
            CanvasUtilities.HideCanvas(canvasGroup);
            CanvasUtilities.HideCanvas(dialoguePanel);
        }

        private new void Show()
        {
            CanvasUtilities.ShowCanvas(canvasGroup);
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

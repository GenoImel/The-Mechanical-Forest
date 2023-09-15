using UnityEngine;
using Akashic.Core;
using Akashic.Runtime.Controllers.Story;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class StoryMonoSystem : MonoBehaviour, IStoryMonoSystem
    {
        [SerializeField] DialogueController dialogueController;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void DisplayStoryEventDialogue(DialogueStoryEventMessage message)
        {
            // pass in one story point to the dialogue controller
            // keep track of which story point in the story event you are on
            // clean up once there are no more story points
            dialogueController.ShowStoryPointDialogue(message.StoryEvent.storyPoints[0]);
        }

        private void AddListeners()
        {
            GameManager.AddListener<DialogueStoryEventMessage>(DisplayStoryEventDialogue);
        }

        private void RemoveListeners()
        {
            GameManager.RemoveListener<DialogueStoryEventMessage>(DisplayStoryEventDialogue);
        }
    }
}

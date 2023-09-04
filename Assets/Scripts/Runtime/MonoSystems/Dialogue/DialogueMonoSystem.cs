using UnityEngine;
using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.Dialogue
{
    internal sealed class DialogueMonoSystem : MonoBehaviour, IDialogueMonoSystem
    {
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void DisplayStoryEventDialogueLine(DialogueStoryEventMessage message)
        {
            Debug.Log(
                $"From inside DialogueMonoSystem: {message.StoryEvent.storyPoints[0].dialogueLine}"
            );
        }

        private void AddListeners()
        {
            GameManager.AddListener<DialogueStoryEventMessage>(DisplayStoryEventDialogueLine);
        }

        private void RemoveListeners()
        {
            GameManager.RemoveListener<DialogueStoryEventMessage>(DisplayStoryEventDialogueLine);
        }
    }
}

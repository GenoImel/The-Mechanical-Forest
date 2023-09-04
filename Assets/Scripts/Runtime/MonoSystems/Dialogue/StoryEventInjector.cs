using UnityEngine;
using Akashic.Core;
using Akashic.ScriptableObjects.Scripts;

namespace Akashic.Runtime.MonoSystems.Dialogue
{
    internal sealed class StoryEventInjector : MonoBehaviour
    {
        public StoryEventBaseData storyEvent;

        private void OnMouseDown()
        {
            GameManager.Publish(new DialogueStoryEventMessage(storyEvent));
        }
    }
}

using UnityEngine;
using Akashic.Core;
using Akashic.ScriptableObjects.Scripts;

namespace Akashic.Runtime.MonoSystems.Story
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

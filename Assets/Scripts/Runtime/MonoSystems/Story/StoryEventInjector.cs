using UnityEngine;
using Akashic.Core;
using Akashic.ScriptableObjects.StoryBase;
using UnityEngine.EventSystems;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class StoryEventInjector : MonoBehaviour
    {
        public StoryEventBaseData storyEvent;

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            GameManager.Publish(new DialogueStoryEventMessage(storyEvent));
        }
    }
}

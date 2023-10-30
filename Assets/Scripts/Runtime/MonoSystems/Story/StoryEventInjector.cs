using UnityEngine;
using Akashic.Core;
using Akashic.ScriptableObjects.Story;
using UnityEngine.EventSystems;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class StoryEventInjector : MonoBehaviour
    {
        [SerializeField] private StoryEventData storyEventData;

        private void OnMouseUp()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            GameManager.Publish(new NewStoryEventMessage(storyEventData));
        }
    }
}

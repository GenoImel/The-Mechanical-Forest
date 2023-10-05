using UnityEngine;
using Akashic.Core;
using Akashic.ScriptableObjects.StoryBase;
using UnityEngine.EventSystems;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class StoryEventInjector : MonoBehaviour
    {
        [SerializeField] private StoryEventBaseData storyEventBaseData;

        public StoryEventBaseData GetCurrentStoryEvent()
        {
            //var tempStoryEvent = new StoryEvent(storyEventBaseData.storyPoints);
            return storyEventBaseData;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            GameManager.Publish(new NewStoryEventMessage(storyEventBaseData));
        }
    }
}

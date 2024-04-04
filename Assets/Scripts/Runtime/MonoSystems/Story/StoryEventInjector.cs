using Akashic.Core;
using Akashic.Runtime.ScriptableObjects.Story;
using Akashic.Runtime.StateMachines.StoryStates;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class StoryEventInjector : MonoBehaviour
    {
        [SerializeField] private StoryEventData storyEventData;

        private IStoryStateMachine storyState;

        private void Awake()
        {
            storyState = GameManager.GetStateMachine<IStoryStateMachine>();
        }

        private void OnMouseUp()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (storyState.CurrentState is StoryFiniteState.Active)
            {
                return;
            }

            storyState.SetActiveState();
            GameManager.Publish(new NewStoryEventMessage(storyEventData));
        }
    }
}

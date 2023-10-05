using Akashic.Core;
using Akashic.ScriptableObjects.StoryBase;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class StoryMonoSystem : MonoBehaviour, IStoryMonoSystem
    {
        [SerializeField] private StoryEventInjector activeStoryInjector;
        private StoryEventBaseData activeStoryEvent;
        private int storyPointIndex = 0;

        private StoryEvent currentStoryEvent;
        
        private void OnEnable()
        { 
            AddListeners();
        }
        
        private void OnDisable()
        {
            RemoveListeners();
        }
        
        public StoryPoint GetCurrentStoryPoint()
        {
            /*if (activeStoryEvent == null || !activeStoryEvent.storyPoints.Any())
            {
                activeStoryEvent = activeStoryInjector.GetCurrentStoryEvent();
            }
            
            return activeStoryEvent.storyPoints[storyPointIndex];*/
            
            return currentStoryEvent.storyPoints[storyPointIndex];
        }

        private void OnNewStoryEventMessage(NewStoryEventMessage message)
        {
            currentStoryEvent = new StoryEvent(message.StoryEventBaseData.storyPoints);
            storyPointIndex = 0;
            GameManager.Publish(new StoryEventAvailableMessage());
        }
        
        private void AddListeners()
        {
            GameManager.AddListener<NewStoryEventMessage>(OnNewStoryEventMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<NewStoryEventMessage>(OnNewStoryEventMessage);
        }
    }
}
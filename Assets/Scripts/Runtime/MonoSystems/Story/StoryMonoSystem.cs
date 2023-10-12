using System;
using System.Collections.Generic;
using System.Linq;
using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Story
{
    internal sealed class StoryMonoSystem : MonoBehaviour, IStoryMonoSystem
    {
        private StoryEvent currentStoryEvent;
        private int storyPointIndex = 0;
        
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
            if (currentStoryEvent == null || !currentStoryEvent.storyPoints.Any())
            {
                throw new Exception($"{currentStoryEvent} cannot be null or empty.");
            }

            GameManager.Publish(new DialogueEntryAvailableMessage(currentStoryEvent.storyPoints[storyPointIndex]));

            return currentStoryEvent.storyPoints[storyPointIndex];
        }

        public void AdvanceStoryPoint()
        {
            storyPointIndex++;
            if (HasStoryEventEnded() == false)
            {
                GameManager.Publish(new StoryEventAvailableMessage());
            };
        }

        private bool HasStoryEventEnded()
        {
            if (storyPointIndex >= currentStoryEvent.storyPoints.Count)
            {
                currentStoryEvent = new StoryEvent(new List<StoryPoint>());
                storyPointIndex = 0;
                GameManager.Publish(new StoryEventEndedMessage());
                return true;
            }
            return false;
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
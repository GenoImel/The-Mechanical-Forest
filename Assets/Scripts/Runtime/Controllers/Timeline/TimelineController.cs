using Akashic.Core;
using Akashic.Runtime.StateMachines.TurnStates;
using UnityEngine;

namespace Akashic.Runtime.Controllers.Timeline
{
    internal sealed class TimelineController : MonoBehaviour
    {
        [SerializeField] private TimelineContainer timelineContainer;
        
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnTurnStateChangedMessage(TurnStateChangedMessage message)
        {
            if (message.NextState is TurnFiniteState.Promise)
            {
                timelineContainer.ReserveSlotsForParty();
            }
        }
        
        private void AddListeners()
        {
            GameManager.AddListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
        }
        
        private void RemoveListeners()
        {
            GameManager.RemoveListener<TurnStateChangedMessage>(OnTurnStateChangedMessage);
        }
    }
}
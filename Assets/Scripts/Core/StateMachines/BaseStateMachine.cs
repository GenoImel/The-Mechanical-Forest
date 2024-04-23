using System;
using Akashic.Core.Messages;
using UnityEngine;

namespace Akashic.Core.StateMachines
{
    internal abstract class BaseStateMachine<TMessage> : MonoBehaviour, IStateMachine
    where TMessage : IMessage
    {
        private IFiniteState currentState;
        private IFiniteState prevState;

        public IFiniteState CurrentState => currentState;

        /// <summary>
        /// Sets the next state of the State Machine and publishes a State Changed Message.
        /// Used to enforce specific state change pattern across the application.
        /// </summary>
        /// <param name="nextState"></param>
        protected void SetState(IFiniteState nextState)
        {
            if (nextState == null)
            {
                throw new Exception("Next state is null.");
            }

            if (currentState == nextState)
            {
                Debug.LogWarning($"State Machine is already in \"{nextState}\" state.");
                return;
            }
            
            if (currentState != null && currentState.GetFiniteStateType() != nextState.GetFiniteStateType())
            {
                throw new Exception($"Invalid state transition from \"{currentState}\" to \"{nextState}\".");
            }

            prevState = currentState;
            
            var stateChangedMessage = CreateStateChangedMessage(prevState, nextState);
            
            currentState = nextState;
            Debug.Log($"State Machine is now in \"{nextState}\" state.");
            
            GameManager.Publish(stateChangedMessage);
        }

        /// <summary>
        /// Sets the initial state of the State Machine.
        /// Must be called during Awake().
        /// </summary>
        protected abstract void SetInitialState();

        /// <summary>
        /// Creates a State Changed Message while enforcing adherence of a state change pattern
        /// that communicates specifically <paramref name="prevState"/> and <paramref name="nextState"/>.
        /// </summary>
        protected abstract TMessage CreateStateChangedMessage(IFiniteState prevState, IFiniteState nextState);
    }
}
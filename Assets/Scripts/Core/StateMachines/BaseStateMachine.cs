using System;
using Akashic.Core.Messages;
using UnityEngine;

namespace Akashic.Core.StateMachines
{
    internal abstract class BaseStateMachine : MonoBehaviour, IStateMachine
    {
        private IFiniteState currentState;
        private IFiniteState prevState;

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
            
            if (currentState != null && currentState.GetStateType() != nextState.GetStateType())
            {
                throw new Exception($"Invalid state transition from \"{currentState}\" to \"{nextState}\".");
            }

            prevState = currentState;
            
            var stateChangedMessage = CreateStateChangedMessage(prevState, nextState);
            GameManager.Publish(stateChangedMessage);
            
            currentState = nextState;
            
            Debug.Log($"State Machine is now in \"{nextState}\" state.");
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
        protected abstract IMessage CreateStateChangedMessage(IFiniteState prevState, IFiniteState nextState);
    }
}
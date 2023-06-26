using UnityEngine;
using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.ExplorationStates
{
    /// <summary>
    /// Handles updating the player's current state while exploring
    /// </summary>
    internal sealed class ExplorationStateMonoSystem : MonoBehaviour, IExplorationStateMonoSystem
    {
        public ExplorationState previousState { get; private set; }
        public ExplorationState currentState { get; private set; }

        public void SetInitializingExplorationState() 
        {
            SetExplorationState(ExplorationState.Initializing);
        }

        public void SetExplorationExplorationState() 
        {
            SetExplorationState(ExplorationState.Exploration);
        }

        public void SetPartyExplorationState() 
        {
            SetExplorationState(ExplorationState.Party);
        }

        public void SetInventoryExplorationState() 
        {
            SetExplorationState(ExplorationState.Inventory);
        }

        public void SetNoneExplorationState() 
        {
            SetExplorationState(ExplorationState.None);
        }

        private void SetExplorationState(ExplorationState newState) 
        {
            if (newState == currentState) 
            {
                Debug.LogError($"Overworld is already in \"{newState}\" state.");
                return;
            }

            previousState = currentState;

            GameManager.Publish(new ExplorationStateChangeMessage(currentState, previousState));
            
            currentState = newState;
        }
    }
}

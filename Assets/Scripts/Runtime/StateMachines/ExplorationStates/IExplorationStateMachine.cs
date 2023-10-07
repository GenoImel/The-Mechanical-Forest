using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.ExplorationStates
{
    /// <summary>
    /// Interface for the <see cref="ExplorationStateMachine"/>.
    /// </summary>
    internal interface IExplorationStateMachine : IStateMachine
    {
        /// <summary>
        /// Set the <see cref="ExplorationState"/> to <see cref="ExplorationFiniteState.Initializing"/>
        /// </summary>
        public void SetInitializingState();
        
        /// <summary>
        /// Set the <see cref="ExplorationState"/> to <see cref="ExplorationFiniteState.Exploration"/>
        /// </summary>
        public void SetExplorationState();
        
        /// <summary>
        /// Set the <see cref="ExplorationState"/> to <see cref="ExplorationFiniteState.Party"/>
        /// </summary>
        public void SetPartyState();
        
        /// <summary>
        /// Set the <see cref="ExplorationState"/> to <see cref="ExplorationFiniteState.Inventory"/>
        /// </summary>
        public void SetInventoryState();
        
        /// <summary>
        /// Set the <see cref="ExplorationState"/> to <see cref="ExplorationFiniteState.None"/>
        /// </summary>
        public void SetNoneState();
    }
}
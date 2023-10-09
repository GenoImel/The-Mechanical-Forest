using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.GameStates
{
    /// <summary>
    /// Interface for the <see cref="GameStateMachine"/>.
    /// This is the superstate for our hierarchical states.
    /// </summary>
    internal interface IGameStateMachine : IStateMachine
    {
        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameFiniteState.MainMenu"/>.
        /// </summary>
        public void SetMainMenuState();

        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameFiniteState.Exploration"/>.
        /// </summary>
        public void SetExplorationState();

        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameFiniteState.Battle"/>.
        /// </summary>
        public void SetBattleState();

        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameFiniteState.Dialogue"/>.
        /// </summary>
        public void SetDialogueState();

        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameFiniteState.Paused"/>.
        /// </summary>
        public void SetPausedState();
    }
}
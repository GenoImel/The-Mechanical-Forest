using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.GameStates
{
    internal interface IGameStateMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameState.MainMenu"/>.
        /// </summary>
        public void SetMainMenuState();

        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameState.Exploration"/>.
        /// </summary>
        public void SetExplorationState();

        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameState.Battle"/>.
        /// </summary>
        public void SetBattleState();

        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameState.Dialogue"/>.
        /// </summary>
        public void SetDialogueState();

        /// <summary>
        /// Set the <see cref="GameState"/> to <see cref="GameState.Paused"/>.
        /// </summary>
        public void SetPausedState();
    }
}
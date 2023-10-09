using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.BattleStates
{
    /// <summary>
    /// Interface for the <see cref="BattleStateMachine"/>.
    /// </summary>
    internal interface IBattleStateMachine : IStateMachine
    {
        /// <summary>
        /// Set the <see cref="BattleState"/> to <see cref="BattleFiniteState.Initializing"/>
        /// </summary>
        public void SetInitializingState();
        
        /// <summary>
        /// Set the <see cref="BattleState"/> to <see cref="BattleFiniteState.Loot"/>
        /// </summary>
        public void SetLootState();
        
        /// <summary>
        /// Set the <see cref="BattleState"/> to <see cref="BattleFiniteState.Victory"/>
        /// </summary>
        public void SetVictoryState();
        
        /// <summary>
        /// Set the <see cref="BattleState"/> to <see cref="BattleFiniteState.GameOver"/>
        /// </summary>
        public void SetGameOverState();
        
        /// <summary>
        /// Set the <see cref="BattleState"/> to <see cref="BattleFiniteState.None"/>
        /// </summary>
        public void SetNoneState();
    }
}
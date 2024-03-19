using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.TurnStates
{
    /// <summary>
    /// Manages the turn state for the flow of battle.
    /// </summary>
    internal interface ITurnStateMachine : IStateMachine
    {
        /// <summary>
        /// Sets the state to Initializing.
        /// </summary>
        public void SetInitializingState();

        /// <summary>
        /// Sets the state to Promise, where we promise the player spaces in the timeline.
        /// </summary>
        public void SetPromiseState();

        /// <summary>
        /// Sets the state to EnemyPlanning, where the enemies plan their actions.
        /// </summary>
        public void SetEnemyPlanningState();

        /// <summary>
        /// Sets the state to PartyPlanning, where the player plans their actions.
        /// </summary>
        public void SetPartyPlanningState();

        /// <summary>
        /// Sets the state to Execution, where the actions are executed in the order of the timline.
        /// </summary>
        public void SetExecutionState();
        
        /// <summary>
        /// Sets the state to PostExecution, where we clean up after the actions are executed.
        /// Damage over time, debuffs, buffs, and resource regeneration are handled here.
        /// </summary>
        public void SetPostExecutionState();

        /// <summary>
        /// Sets the state to None.
        /// </summary>
        public void SetNoneState();
    }
}
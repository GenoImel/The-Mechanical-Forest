using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.StoryStates
{
    internal interface IStoryStateMachine : IStateMachine
    {
        /// <summary>
        /// Returns the current <see cref="StoryFiniteState"/>.
        /// </summary>
        public IFiniteState CurrentState { get; }
        
        /// <summary>
        /// Set the <see cref="StoryState"/> to <see cref="StoryFiniteState.Inactive"/>
        /// </summary>
        public void SetInactiveState();

        /// <summary>
        /// Set the <see cref="StoryState"/> to <see cref="StoryFiniteState.Active"/>
        /// </summary>
        public void SetActiveState();
    }
}

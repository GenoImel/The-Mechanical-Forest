using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.StoryStates
{
    internal sealed class StoryStateMachine : 
        BaseStateMachine<StoryStateChangedMessage>, 
        IStoryStateMachine
    {
        private void Awake()
        {
            SetInitialState();
        }

        public void SetInactiveState()
        {
            SetState(new StoryFiniteState.Inactive());
        }

        public void SetActiveState()
        {
            SetState(new StoryFiniteState.Active());
        }
        
        protected override void SetInitialState()
        {
            SetState(new StoryFiniteState.Inactive());
        }

        protected override StoryStateChangedMessage CreateStateChangedMessage
        (
            IFiniteState prevState, 
            IFiniteState nextState
        )
        {
            return new StoryStateChangedMessage(
                prevState as StoryFiniteState,
                nextState as StoryFiniteState
            );
        }
    }
}
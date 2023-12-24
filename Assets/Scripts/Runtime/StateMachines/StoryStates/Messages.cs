using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.StoryStates
{
    internal sealed class StoryStateChangedMessage : StateChangedMessage<StoryFiniteState>
    {
        public StoryStateChangedMessage(StoryFiniteState prevState, StoryFiniteState nextState)
        {
            PrevState = prevState;
            NextState = nextState;
        }
    }
}

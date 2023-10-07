using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.ExplorationStates
{
    internal sealed class ExplorationStateChangedMessage : StateChangedMessage<ExplorationFiniteState>
    {
        public ExplorationStateChangedMessage(ExplorationFiniteState prevState, ExplorationFiniteState nextState)
        {
            PrevState = prevState;
            NextState = nextState;
        }
    }
}
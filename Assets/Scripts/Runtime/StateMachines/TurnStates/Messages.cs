using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.TurnStates
{
    internal sealed class TurnStateChangedMessage : StateChangedMessage<TurnFiniteState>
    {
        public TurnStateChangedMessage(TurnFiniteState prevState, TurnFiniteState nextState)
        {
            PrevState = prevState;
            NextState = nextState;
        }
    }
}
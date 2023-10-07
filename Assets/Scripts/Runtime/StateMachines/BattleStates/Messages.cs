using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.BattleStates
{
    internal sealed class BattleStateChangedMessage : StateChangedMessage<BattleFiniteState>
    {
        public BattleStateChangedMessage(BattleFiniteState prevState, BattleFiniteState nextState)
        {
            PrevState = prevState;
            NextState = nextState;
        }
    }
}
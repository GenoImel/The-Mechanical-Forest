using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.GameStates
{
    internal sealed class GameStateChangedMessage : StateChangedMessage<GameFiniteState>
    {
        public GameStateChangedMessage(GameFiniteState prevState, GameFiniteState nextState)
        {
            PrevState = prevState;
            NextState = nextState;
        }
    }
}
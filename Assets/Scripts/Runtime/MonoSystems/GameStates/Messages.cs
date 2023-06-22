using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.GameStates
{
    internal sealed class GameStateChangedMessage : IMessage
    {
        public GameState PrevState { get; }
        
        public GameState NextState { get; }

        public GameStateChangedMessage(GameState prevState, GameState nextState)
        {
            PrevState = prevState;
            NextState = nextState;
        }
    }
}
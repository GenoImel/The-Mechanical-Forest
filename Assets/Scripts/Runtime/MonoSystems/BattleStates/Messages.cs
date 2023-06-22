using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.BattleStates
{
    /// <summary>
    /// Communicates a change in a battle's state
    /// </summary>
    internal sealed class BattleStateChangeMessage : IMessage
    {
        public BattleState currentState;
        public BattleState previousState;

        public BattleStateChangeMessage(BattleState _currentState, BattleState _previousState) {
            currentState = _currentState;
            previousState = _previousState;
        }
    }
}

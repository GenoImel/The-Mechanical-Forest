using Akashic.Core.Messages;

namespace Akashic.Runtime.MonoSystems.BattleStates
{
    /// <summary>
    /// Communicates a change in a battle's state
    /// </summary>
    internal sealed class BattleStateChangeMessage : IMessage
    {
        public BattleState PreviousState { get; }
        public BattleState NextState { get; }

        public BattleStateChangeMessage(BattleState nextState, BattleState prevState) 
        {
            NextState = nextState;
            PreviousState = prevState;
        }
    }
}

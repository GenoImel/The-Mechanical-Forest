using Akashic.Core.Messages;

namespace Akashic.Core.StateMachines
{
    /// <summary>
    /// Base Message Event for State Changes.
    /// Enforces specific state change pattern across the application.
    /// </summary>
    internal class StateChangedMessage<TFiniteState> : IMessage where TFiniteState : IFiniteState
    {
        public TFiniteState PrevState { get; protected set; }
        public TFiniteState NextState { get; protected set; }
        
        public StateChangedMessage() { }
    }
}
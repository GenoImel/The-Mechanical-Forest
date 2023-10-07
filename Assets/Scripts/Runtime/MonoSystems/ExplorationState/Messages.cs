using Akashic.Core.Messages;

namespace Akashic.Runtime.MonoSystems.ExplorationStates
{
    /// <summary>
    /// Communicates a state change while exploring
    /// </summary>
    internal sealed class ExplorationStateChangeMessage : IMessage
    {
        public ExplorationState PreviousState { get; }
        public ExplorationState NextState { get; }

        public ExplorationStateChangeMessage(ExplorationState nextState, ExplorationState prevState) 
        {
            NextState = nextState;
            PreviousState = prevState;
        }
    }
}

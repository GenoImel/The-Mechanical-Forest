using Akashic.Core.Messages;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.ExplorationStates
{
    internal sealed class ExplorationStateMachine : BaseStateMachine, IExplorationStateMachine
    {
        private void Awake()
        {
            SetInitialState();
        }
        
        public void SetInitializingState()
        {
            SetState(new ExplorationFiniteState.Initializing());
        }
        
        public void SetExplorationState()
        {
            SetState(new ExplorationFiniteState.Exploration());
        }
        
        public void SetPartyState()
        {
            SetState(new ExplorationFiniteState.Party());
        }
        
        public void SetInventoryState()
        {
            SetState(new ExplorationFiniteState.Inventory());
        }
        
        public void SetNoneState()
        {
            SetState(new ExplorationFiniteState.None());
        }
        
        protected override void SetInitialState()
        {
            SetState(new ExplorationFiniteState.None());
        }
        
        protected override IMessage CreateStateChangedMessage(IFiniteState prevState, IFiniteState nextState)
        {
            return new ExplorationStateChangedMessage(
                prevState as ExplorationFiniteState,
                nextState as ExplorationFiniteState
            );
        }
    }
}
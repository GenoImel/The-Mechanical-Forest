using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.TurnStates
{
    internal sealed class TurnStateMachine :
        BaseStateMachine<TurnStateChangedMessage>,
        ITurnStateMachine
    {
        private void Awake()
        {
            SetInitialState();
        }

        public void SetInitializingState()
        {
            SetState(new TurnFiniteState.Initializing());
        }
        
        public void SetPromiseState()
        {
            SetState(new TurnFiniteState.Promise());
        }
        
        public void SetEnemyPlanningState()
        {
            SetState(new TurnFiniteState.EnemyPlanning());
        }
        
        public void SetPartyPlanningState()
        {
            SetState(new TurnFiniteState.PartyPlanning());
        }
        
        public void SetExecutionState()
        {
            SetState(new TurnFiniteState.Execution());
        }
        
        public void SetPostExecutionState()
        {
            SetState(new TurnFiniteState.PostExecution());
        }
        
        public void SetNoneState()
        {
            SetState(new TurnFiniteState.None());
        }
        
        protected override void SetInitialState()
        {
            SetState(new TurnFiniteState.Initializing());
        }
        
        protected override TurnStateChangedMessage CreateStateChangedMessage
        (
            IFiniteState prevState, 
            IFiniteState nextState
        )
        {
            return new TurnStateChangedMessage(
                prevState as TurnFiniteState,
                nextState as TurnFiniteState
            );
        }
    }
}
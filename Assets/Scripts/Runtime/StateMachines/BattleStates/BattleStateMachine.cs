using Akashic.Core.Messages;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.BattleStates
{
    internal sealed class BattleStateMachine : BaseStateMachine, IBattleStateMachine
    {
        private void Awake()
        {
            SetInitialState();
        }
        
        public void SetInitializingState()
        {
            SetState(new BattleFiniteState.Initializing());
        }
        
        public void SetBattleActiveState()
        {
            SetState(new BattleFiniteState.BattleActive());
        }
        
        public void SetLootState()
        {
            SetState(new BattleFiniteState.Loot());
        }

        public void SetVictoryState()
        {
            SetState(new BattleFiniteState.Victory());
        }
        
        public void SetGameOverState()
        {
            SetState(new BattleFiniteState.GameOver());
        }
        
        public void SetNoneState()
        {
            SetState(new BattleFiniteState.None());
        }
        
        protected override void SetInitialState()
        {
            SetState(new BattleFiniteState.None());
        }
        
        protected override IMessage CreateStateChangedMessage(IFiniteState prevState, IFiniteState nextState)
        {
            return new BattleStateChangedMessage(
                prevState as BattleFiniteState,
                nextState as BattleFiniteState
            );
        }
    }
}
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.GameStates
{
    internal sealed class GameStateMachine : 
        BaseStateMachine<GameStateChangedMessage>, 
        IGameStateMachine
    {
        private void Awake()
        {
            SetInitialState();
        }

        public void SetMainMenuState()
        {
            SetState(new GameFiniteState.MainMenu());
        }

        public void SetExplorationState()
        {
            SetState(new GameFiniteState.Exploration());
        }

        public void SetBattleState()
        {
            SetState(new GameFiniteState.Battle());
        }

        public void SetDialogueState()
        {
            SetState(new GameFiniteState.Dialogue());
        }

        public void SetPausedState()
        {
            SetState(new GameFiniteState.Paused());
        }

        protected override void SetInitialState()
        {
            SetState(new GameFiniteState.MainMenu());
        }

        protected override GameStateChangedMessage CreateStateChangedMessage
        (
            IFiniteState prevState, 
            IFiniteState nextState
        )
        {
            return new GameStateChangedMessage(
                prevState as GameFiniteState,
                nextState as GameFiniteState
            );
        }
    }
}
using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.GameStates
{
    internal sealed class GameStateMonoSystem : MonoBehaviour, IGameStateMonoSystem
    {
        private GameState currentState = GameState.MainMenu;
        private GameState prevState;

        public void SetMainMenuState()
        {
            SetState(GameState.MainMenu);
        }

        public void SetExplorationState()
        {
            SetState(GameState.Exploration);
        }

        public void SetBattleState()
        {
            SetState(GameState.Battle);
        }

        public void SetDialogueState()
        {
            SetState(GameState.Dialogue);
        }

        public void SetPausedState()
        {
            SetState(GameState.Paused);
        }

        private void SetState(GameState nextState)
        {
            if (currentState == nextState)
            {
                Debug.LogWarning($"Game is already in \"{nextState}\" state.");
                return;
            }

            prevState = currentState;
            GameManager.Publish(new GameStateChangedMessage(prevState, nextState));
            currentState = nextState;
        }
    }
}
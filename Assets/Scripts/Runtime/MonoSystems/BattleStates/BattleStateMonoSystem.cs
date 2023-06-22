using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.BattleStates
{
	/// <summary>
    /// Handles updating a battle's state
    /// </summary>
    internal sealed class BattleStateMonoSystem : MonoBehaviour, IBattleStateMonoSystem
    {
        public BattleState currentState { get; private set; } = BattleState.Initializing;
        public BattleState previousState { get; private set; }

        public void SetInitializingBattleState()
        {
        	SetBattleState(BattleState.Initializing);
        }

        public void SetLootBattleState()
        {
        	SetBattleState(BattleState.Loot);
        }

        public void SetNoneBattleState()
        {
        	SetBattleState(BattleState.None);
        }

        public void SetVictoryBattleState()
        {
        	SetBattleState(BattleState.Victory);
        }

        public void SetGameOverBattleState()
        {
        	SetBattleState(BattleState.GameOver);
        }

        private void SetBattleState(BattleState state)
        {
        	if (state == currentState) 
        	{
        		Debug.LogWarning($"Game is already in \"{state}\" state.");
        		return;
        	}

        	previousState = currentState;
        	currentState = state;

        	GameManager.Publish(new BattleStateChangeMessage(currentState, previousState));
        }
    }
}

using Akashic.Core;
using Akashic.Core.MonoSystems;

namespace Akashic.Runtime.MonoSystems.BattleStates
{
    internal interface IBattleStateMonoSystem : IMonoSystem
    {
    	public void SetInitializingBattleState();
    	public void SetLootBattleState();
    	public void SetNoneBattleState();
    	public void SetVictoryBattleState();
    	public void SetGameOverBattleState();
    }
}

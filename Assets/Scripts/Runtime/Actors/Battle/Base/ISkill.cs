using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.MonoSystems.Battle;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal interface ISkill
    {
        private IPartyBattleMonoSystem PartyMonoSystem => 
            GameManager.GetMonoSystem<IPartyBattleMonoSystem>();
        
        private IEnemyBattleMonoSystem EnemyBattleMonoSystem => 
            GameManager.GetMonoSystem<IEnemyBattleMonoSystem>();
        
        public void Execute(BattleActor source, List<BattleActor> target);
    }
}
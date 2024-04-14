using System.Collections.Generic;
using Akashic.Core.MonoSystems;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Enemy;
using Akashic.Runtime.ScriptableObjects.Battle;

namespace Akashic.Runtime.MonoSystems.Battle
{
    internal interface IEnemyBattleMonoSystem : IMonoSystem
    {
        public EncounterData EncounterData { get; }
        
        public void SetEncounterData(EncounterData data);
        
        public void AddEnemyBattleActor(EnemyBattleActor enemyBattleActor);
        
        public void InitializeAbilityPoints();

        public void SortEnemyBattleActorsBySpeed();
        
        public List<BattleActor> GetBattleActorsAsBase();
        
        public List<EnemyBattleActor> GetBattleActors();
    }
}
using Akashic.Core.MonoSystems;
using Akashic.Runtime.Actors.Battle;
using Akashic.Runtime.Actors.Battle.Enemy;
using Akashic.ScriptableObjects.Battle;

namespace Akashic.Runtime.MonoSystems.Battle
{
    internal interface IEnemyBattleMonoSystem : IMonoSystem
    {
        public EncounterData EncounterData { get; }
        
        public void SetEncounterData(EncounterData data);
        
        public void AddEnemyBattleActor(EnemyBattleActor enemyBattleActor);
        
        public void InitializeAbilityPoints();

        public void SortEnemyBattleActorsBySpeed();
    }
}
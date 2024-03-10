using Akashic.Runtime.Serializers.Save;
using Akashic.ScriptableObjects.Battle;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class BattleActorInitializationParameters
    {
        public PartyMember PartyMember { get; set; }
        public EnemyData EnemyData { get; set; }
        public PartyBattleActor PartyBattleActor { get; set; }
        public EnemyBattleActor EnemyBattleActor { get; set; }
        
        public BattleActorInitializationParameters SetPartyMember(PartyMember partyMember)
        {
            PartyMember = partyMember;
            return this;
        }
        
        public BattleActorInitializationParameters SetEnemyData(EnemyData enemyData)
        {
            EnemyData = enemyData;
            return this;
        }
        
        public BattleActorInitializationParameters SetPartyBattleActor(PartyBattleActor partyBattleActor)
        {
            PartyBattleActor = partyBattleActor;
            return this;
        }
        
        public BattleActorInitializationParameters SetEnemyBattleActor(EnemyBattleActor enemyBattleActor)
        {
            EnemyBattleActor = enemyBattleActor;
            return this;
        }
    }
}
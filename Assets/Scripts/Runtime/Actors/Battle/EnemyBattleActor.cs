using Akashic.ScriptableObjects.Battle;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class EnemyBattleActor : BattleActor
    {
        private EnemyData enemyData;
        
        public EnemyData EnemyData => enemyData;
        
        public void InitializeEnemyBattleActor(EnemyData enemyData)
        {
            this.enemyData = enemyData;
            
            var parameters = new BattleActorInitializationParameters();
            parameters.SetEnemyData(enemyData);
            parameters.SetEnemyBattleActor(this);
            
            statHandler.InitializeBattleActorStats(parameters);
        }
    }
}
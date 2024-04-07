using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Environment;
using Akashic.Runtime.ScriptableObjects.Battle;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Enemy
{
    internal sealed class EnemyBattleActor : BattleActor
    {
        private EnemyData enemyData;
        
        public EnemyData EnemyData => enemyData;

        public BaseEnemyBehaviour enemyBehaviour;
        
        public void InitializeEnemyBattleActor(EnemyData enemyData)
        {
            this.enemyData = enemyData;
            
            var parameters = new BattleActorInitializationParameters();
            parameters.SetEnemyData(enemyData);
            parameters.SetEnemyBattleActor(this);
            
            statHandler.InitializeBattleActorStats(parameters);
            InitializeEnemyBehaviour();
        }

        private void InitializeEnemyBehaviour()
        {
            enemyBehaviour = Instantiate(EnemyData.enemyBehaviour, transform);
            enemyBehaviour.SetSourceBattleActor(this);
            enemyBehaviour.SetBaseSkills();
        }
    }
}
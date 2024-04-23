using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Environment;
using Akashic.Runtime.MonoSystems.Battle;
using Akashic.Runtime.ScriptableObjects.Battle;
using Akashic.Runtime.Serializers.Save;
using Akashic.Runtime.Utilities.GameMath;

namespace Akashic.Runtime.Actors.Battle.Enemy
{
    internal sealed class EnemyBattleActorStatHandler : BattleActorStatHandler
    {
        private EnemyBattleActor enemyBattleActor;
        
        private IEnemyBattleMonoSystem enemyBattleMonoSystem;

        protected override void Awake()
        {
            base.Awake();
            
            enemyBattleMonoSystem = GameManager.GetMonoSystem<IEnemyBattleMonoSystem>();
        }

        public override void InitializeBattleActorStats(BattleActorInitializationParameters parameters)
        {
            SetEnemyStats(parameters.EnemyData);
            
            SetBattleActorReference(parameters.EnemyBattleActor);
            RegeneratePips();
            RefreshBufferHitPoints();
        }

        private void SetEnemyStats(EnemyData enemyData)
        {
            currentLevel = enemyBattleMonoSystem.EncounterData.scaledEncounterLevel;
            
            var totalHitPoints = StatsMath.CalculateMaxHitPoints(enemyData, currentLevel);
            hitPoints = new HitPoints(totalHitPoints, totalHitPoints);
            
            var totalMight = StatsMath.CalculateTotalMight(enemyData, currentLevel);
            might = new Might(totalMight, totalMight);
            
            var totalDeftness = StatsMath.CalculateTotalDeftness(enemyData, currentLevel);
            deftness = new Deftness(totalDeftness, totalDeftness);
            
            var totalTenacity = StatsMath.CalculateTotalTenacity(enemyData, currentLevel);
            tenacity = new Tenacity(totalTenacity, totalTenacity);
            
            var totalResolve = StatsMath.CalculateTotalResolve(enemyData, currentLevel);
            resolve = new Resolve(totalResolve, totalResolve);
            
            var totalAbilityPoints = ResourcesMath.CalculateAbilityPoints(enemyData, currentLevel);
            baseAbilityPoints = totalAbilityPoints;
        }
        
        private void SetBattleActorReference(EnemyBattleActor battleActor)
        {
            enemyBattleActor = battleActor;
        }
        
        protected override void RefreshBufferHitPoints()
        {
            //TO DO: Only generate these when defend action is chosen
            bufferHitPoints = ResourcesMath.CalculateBufferHitPoints(enemyBattleActor);
        }
    }
}
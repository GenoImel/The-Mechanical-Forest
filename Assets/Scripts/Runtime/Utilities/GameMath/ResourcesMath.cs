using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Actors.Battle.Enemy;
using Akashic.Runtime.Actors.Battle.Party;
using Akashic.Runtime.ScriptableObjects.Battle;
using Akashic.Runtime.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.Utilities.GameMath
{
    internal static class ResourcesMath
    {
        public static int CalculateAbilityPoints(PartyMemberData partyMemberData)
        {
            var totalDeftness = StatsMath.CalculateTotalDeftness(partyMemberData);
            
            var totalResolve = StatsMath.CalculateTotalResolve(partyMemberData);

            var baseAbilityPoints = partyMemberData.baseLevel + (2 * totalDeftness) + (2 * totalResolve);

            baseAbilityPoints += StatsMath.CalculateEquipmentAbilityPointsBonus(partyMemberData);

            return baseAbilityPoints;
        }
        
        public static int CalculateAbilityPoints(EnemyData enemyData, int scaledLevel)
        {
            var totalDeftness = StatsMath.CalculateTotalDeftness(enemyData, scaledLevel);
            
            var totalResolve = StatsMath.CalculateTotalResolve(enemyData, scaledLevel);

            var baseAbilityPoints = scaledLevel + (2 * totalDeftness) + (2 * totalResolve);

            return baseAbilityPoints;
        }

        public static int CalculateAbilityPointsToRegenerate(List<PartyBattleActor> partyBattleActors)
        {
            var totalAbilityPointsRegenerated = 0;

            foreach (var battleActor in partyBattleActors)
            {
                var c1 = Mathf.FloorToInt(0.10f * battleActor.statHandler.BaseAbilityPoints);
                c1 = Mathf.Max(c1, 1); 
                
                var c2 = Mathf.FloorToInt(0.50f * battleActor.statHandler.CurrentTenacity);
                c2 = Mathf.Max(c2, 1); 
                
                totalAbilityPointsRegenerated += battleActor.statHandler.CurrentLevel 
                                                 + c1 
                                                 + battleActor.statHandler.ActionPips * c2;

                totalAbilityPointsRegenerated += StatsMath.CalculateEquipmentAbilityPointsRegenBonus(battleActor);
            }

            return totalAbilityPointsRegenerated;
        }

        public static int CalculateTotalPooledAbilityPoints(List<PartyBattleActor> partyBattleActors)
        {
            return partyBattleActors.Sum(battleActor => battleActor.statHandler.BaseAbilityPoints);
        }
        
        public static int CalculateTotalPooledAbilityPoints(List<EnemyBattleActor> enemyBattleActors)
        {
            return enemyBattleActors.Sum(battleActor => battleActor.statHandler.BaseAbilityPoints);
        }
        
        public static int CalculateBufferHitPoints(PartyBattleActor partyBattleActor)
        {
            var calculatedBufferHitPoints = 
                (2 * partyBattleActor.statHandler.CurrentTenacity)
                + partyBattleActor.statHandler.CurrentDeftness
                + UnityEngine.Random.Range(3, (3 + partyBattleActor.statHandler.CurrentLevel));

            if (partyBattleActor.equipmentHandler.ArmorData != null)
            {
                calculatedBufferHitPoints += partyBattleActor.equipmentHandler.ArmorData.bufferHitPointIncrease;
            }
            
            return calculatedBufferHitPoints;
        }

        public static int CalculateBufferHitPoints(EnemyBattleActor enemyBattleActor)
        {
            var calculatedBufferHitPoints = 
                (2 * enemyBattleActor.statHandler.CurrentTenacity)
                + enemyBattleActor.statHandler.CurrentDeftness
                + UnityEngine.Random.Range(3, (3 + enemyBattleActor.statHandler.CurrentLevel));
            
            return calculatedBufferHitPoints;
        }
    }
}

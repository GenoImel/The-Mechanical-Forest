using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Actors.Battle;
using Akashic.Runtime.Utilities.GameMath.Stats;
using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.Utilities.GameMath.Resources
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
        
        public static int CalculateBufferHitPoints(PartyBattleActor partyBattleActor)
        {
            var calculatedBufferHitPoints = 
                (2 * partyBattleActor.statHandler.CurrentTenacity)
                + partyBattleActor.statHandler.CurrentDeftness
                + UnityEngine.Random.Range(3, (3 + partyBattleActor.statHandler.CurrentLevel));
            
            calculatedBufferHitPoints += partyBattleActor.partyMemberData.armor.bufferHitPointIncrease;

            return calculatedBufferHitPoints;
        }


    }
}

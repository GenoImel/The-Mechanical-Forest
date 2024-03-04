using System;
using System.Linq;
using Akashic.Runtime.Actors.Battle;
using Akashic.ScriptableObjects.Inventory;
using Akashic.ScriptableObjects.PartyMember;

namespace Akashic.Runtime.Utilities.GameMath.Stats
{
    internal static class StatsMath
    {
        public static int CalculateMaxHitPoints(PartyMemberData partyMemberData)
        {
            var totalMight = CalculateTotalMight(partyMemberData);
            
            var totalResolve = CalculateTotalResolve(partyMemberData);
            
            var totalTenacity = CalculateTotalTenacity(partyMemberData);
            
            var totalCalculatedHitPoints = 10 + (5 * partyMemberData.baseLevel) 
                                                 + (2 * totalMight) 
                                                 + (2 * totalResolve) 
                                                 + (4 * totalTenacity);
            
            totalCalculatedHitPoints += CalculateEquipmentMaxHitPointBonus(partyMemberData);

            return totalCalculatedHitPoints;
        }
        
        public static int CalculateTotalMight(PartyMemberData partyMemberData)
        {
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.might, 
                armor => armor.might, 
                partyMemberData.baseMight
            );
        }
        
        public static int CalculateTotalDeftness(PartyMemberData partyMemberData)
        {
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.deftness, 
                armor => armor.deftness, 
                partyMemberData.baseDeftness
            );
        }
        
        public static int CalculateTotalTenacity(PartyMemberData partyMemberData)
        {
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.tenacity, 
                armor => armor.tenacity, 
                partyMemberData.baseTenacity
            );
        }
        
        public static int CalculateTotalResolve(PartyMemberData partyMemberData)
        {
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.resolve, 
                armor => armor.resolve, 
                partyMemberData.baseResolve
            );
        }
        
        public static int CalculateEquipmentAbilityPointsBonus(PartyMemberData partyMemberData)
        {
            return StatsMath.CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.baseAbilityPoints, 
                armor => armor.baseAbilityPoints
            );
        }
        
        public static int CalculateEquipmentAbilityPointsRegenBonus(PartyBattleActor partyBattleActor)
        {
            return StatsMath.CalculateTotalStat(
                partyBattleActor.partyMemberData, 
                accessory => accessory.baseAbilityPointsRegen, 
                armor => armor.baseAbilityPointsRegen
            );
        }
        
        private static int CalculateEquipmentMaxHitPointBonus(PartyMemberData partyMemberData)
        {
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.maxHitPoints, 
                armor => armor.maxHitPoints, 
                0
            );
        }

        private static int CalculateTotalStat(
            PartyMemberData partyMemberData, 
            Func<AccessoryData, StatModifier> accessoryStatSelector, 
            Func<ArmorData, StatModifier> armorStatSelector, 
            int baseStat)
        {
            var flatBonus = CalculateFlatBonus(partyMemberData, accessoryStatSelector, armorStatSelector);
            var percentBonus = CalculatePercentBonus(partyMemberData, accessoryStatSelector, armorStatSelector);

            var totalStat = baseStat + flatBonus;
            totalStat *= (int)(1 + percentBonus / 100.0);
            return totalStat;
        }

        private static int CalculateTotalStat(
            PartyMemberData partyMemberData, 
            Func<AccessoryData, StatModifier> accessoryStatSelector, 
            Func<ArmorData, StatModifier> armorStatSelector)
        {

            var flatBonus = CalculateFlatBonus(partyMemberData, accessoryStatSelector, armorStatSelector);
            var percentBonus = CalculatePercentBonus(partyMemberData, accessoryStatSelector, armorStatSelector);
            
            var totalStat = flatBonus;
            totalStat *= (int)(1 + percentBonus / 100.0);
            return totalStat;
        }

        private static int CalculateFlatBonus(
            PartyMemberData partyMemberData,
            Func<AccessoryData, StatModifier> accessoryStatSelector,
            Func<ArmorData, StatModifier> armorStatSelector)
        {
            var flatBonus = 0;
            
            if (partyMemberData.accessories != null)
            {
                flatBonus += partyMemberData.accessories
                    .Where(accessory => accessory != null)
                    .Sum(accessory => accessoryStatSelector(accessory).modifierType == StatModifierType.Flat 
                        ? accessoryStatSelector(accessory).value 
                        : 0);
            }

            if (partyMemberData.armor != null)
            {
                var armorStat = armorStatSelector(partyMemberData.armor);
                if (armorStat.modifierType == StatModifierType.Flat)
                {
                    flatBonus += armorStat.value;
                }
            }

            return flatBonus;
        }

        private static int CalculatePercentBonus(
            PartyMemberData partyMemberData,
            Func<AccessoryData, StatModifier> accessoryStatSelector,
            Func<ArmorData, StatModifier> armorStatSelector)
        {
            var percentBonus = 0;
            
            if (partyMemberData.accessories != null)
            {
                percentBonus += partyMemberData.accessories
                    .Where(accessory => accessory != null)
                    .Sum(accessory => accessoryStatSelector(accessory).modifierType == StatModifierType.Percent 
                        ? accessoryStatSelector(accessory).value 
                        : 0);
            }

            if (partyMemberData.armor != null)
            {
                var armorStat = armorStatSelector(partyMemberData.armor);
                if (armorStat.modifierType == StatModifierType.Percent)
                {
                    percentBonus += armorStat.value;
                }
            }
            
            return percentBonus;
        }
    }
}

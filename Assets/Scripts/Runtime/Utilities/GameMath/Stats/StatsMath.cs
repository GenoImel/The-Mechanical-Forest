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

        private static int CalculateTotalStat(
            PartyMemberData partyMemberData, 
            Func<AccessoryData, StatModifier> accessoryStatSelector, 
            Func<ArmorData, StatModifier> armorStatSelector, 
            int baseStat)
        {
            var flatBonus = partyMemberData.accessories
                                .Sum(accessory => 
                                    accessoryStatSelector(accessory).modifierType == StatModifierType.Flat 
                                    ? accessoryStatSelector(accessory).value 
                                    : 0)
                            + (armorStatSelector(partyMemberData.armor).modifierType == StatModifierType.Flat 
                                ? armorStatSelector(partyMemberData.armor).value 
                                : 0);
            
            var percentBonus = partyMemberData.accessories
                                   .Sum(accessory => 
                                       accessoryStatSelector(accessory).modifierType == StatModifierType.Percent 
                                       ? accessoryStatSelector(accessory).value 
                                       : 0)
                               + (armorStatSelector(partyMemberData.armor).modifierType == StatModifierType.Percent 
                                   ? armorStatSelector(partyMemberData.armor).value 
                                   : 0);

            var totalStat = baseStat + flatBonus;
            totalStat += (int)(totalStat * (percentBonus / 100.0));
            return totalStat;
        }

        private static int CalculateTotalStat(
            PartyMemberData partyMemberData, 
            Func<AccessoryData, StatModifier> accessorySelector, 
            Func<ArmorData, StatModifier> armorSelector)
        {
            var flatBonus = partyMemberData.accessories
                                .Sum(accessory => 
                                    accessorySelector(accessory).modifierType == StatModifierType.Flat 
                                        ? accessorySelector(accessory).value 
                                        : 0)
                            + (armorSelector(partyMemberData.armor).modifierType == StatModifierType.Flat 
                                ? armorSelector(partyMemberData.armor).value 
                                : 0);
            
            var percentBonus = partyMemberData.accessories
                                   .Sum(accessory => 
                                       accessorySelector(accessory).modifierType == StatModifierType.Percent 
                                           ? accessorySelector(accessory).value 
                                           : 0)
                               + (armorSelector(partyMemberData.armor).modifierType == StatModifierType.Percent 
                                   ? armorSelector(partyMemberData.armor).value 
                                   : 0);

            var totalBonus = flatBonus;
            
            if (percentBonus > 0)
            {
                totalBonus += (int)(flatBonus * (percentBonus / 100.0));
            }

            return totalBonus;
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
    }
}

using System;
using System.Linq;
using Akashic.Core;
using Akashic.Runtime.Actors.Battle;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.ScriptableObjects.Battle;
using Akashic.ScriptableObjects.Inventory;
using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.Utilities.GameMath.Stats
{
    internal static class StatsMath
    {
        private static IConfigMonoSystem ConfigMonoSystem => GameManager.GetMonoSystem<IConfigMonoSystem>();
        
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
        
        public static int CalculateMaxHitPoints(EnemyData enemyData, int scaledLevel)
        {
            var totalMight = CalculateTotalMight(enemyData, scaledLevel);
            
            var totalResolve = CalculateTotalResolve(enemyData, scaledLevel);
            
            var totalTenacity = CalculateTotalTenacity(enemyData, scaledLevel);
            
            var totalCalculatedHitPoints = 10 + (5 * scaledLevel) 
                                                 + (2 * totalMight) 
                                                 + (2 * totalResolve) 
                                                 + (4 * totalTenacity);
            
            return totalCalculatedHitPoints;
        }
        
        public static int CalculateTotalMight(PartyMemberData partyMemberData)
        {
            var scaledMight = CalculateScaledStat(partyMemberData.maxMight, partyMemberData.baseLevel);
            
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.might, 
                armor => armor.might, 
                scaledMight
            );
        }

        public static int CalculateTotalMight(EnemyData enemyData, int scaledLevel)
        {
            var maxMight = enemyData.enemyClass.maxMight;
            maxMight += enemyData.mightModifier;
            
            var scaledMight = CalculateScaledStat(maxMight, scaledLevel);
            return scaledMight;
        }

        public static int CalculateTotalDeftness(PartyMemberData partyMemberData)
        {
            var scaledDeftness = CalculateScaledStat(partyMemberData.maxDeftness, partyMemberData.baseLevel);
            
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.deftness, 
                armor => armor.deftness, 
                scaledDeftness
            );
        }
        
        public static int CalculateTotalDeftness(EnemyData enemyData, int scaledLevel)
        {
            var maxDeftness = enemyData.enemyClass.maxDeftness;
            maxDeftness += enemyData.deftnessModifier;
            
            var scaledDeftness = CalculateScaledStat(maxDeftness, scaledLevel);
            return scaledDeftness;
        }

        public static int CalculateTotalTenacity(PartyMemberData partyMemberData)
        {
            var scaledTenacity = CalculateScaledStat(partyMemberData.maxTenacity, partyMemberData.baseLevel);
            
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.tenacity, 
                armor => armor.tenacity, 
                scaledTenacity
            );
        }
        
        public static int CalculateTotalTenacity(EnemyData enemyData, int scaledLevel)
        {
            var maxTenacity = enemyData.enemyClass.maxTenacity;
            maxTenacity += enemyData.tenacityModifier;
            
            var scaledTenacity = CalculateScaledStat(maxTenacity, scaledLevel);
            return scaledTenacity;
        }
        
        public static int CalculateTotalResolve(PartyMemberData partyMemberData)
        {
            var scaledResolve = CalculateScaledStat(partyMemberData.maxResolve, partyMemberData.baseLevel);
            
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.resolve, 
                armor => armor.resolve, 
                scaledResolve
            );
        }
        
        public static int CalculateTotalResolve(EnemyData enemyData, int scaledLevel)
        {
            var maxResolve = enemyData.enemyClass.maxResolve;
            maxResolve += enemyData.resolveModifier;
            
            var scaledResolve = CalculateScaledStat(maxResolve, scaledLevel);
            return scaledResolve;
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
                partyBattleActor, 
                accessory => accessory.baseAbilityPointsRegen, 
                armor => armor.baseAbilityPointsRegen
            );
        }
        
        private static int CalculateScaledStat(int maxStat, int currentLevel)
        {
            var scaledStat = Mathf.CeilToInt(
                (currentLevel / ConfigMonoSystem.GetBattleConfigData().maximumLevel) 
                                             * maxStat);
            scaledStat = Mathf.Max(scaledStat, 1);
            return scaledStat;
        }
        
        private static int CalculateEquipmentMaxHitPointBonus(PartyMemberData partyMemberData)
        {
            return CalculateTotalStat(
                partyMemberData, 
                accessory => accessory.maxHitPoints, 
                armor => armor.maxHitPoints
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

        private static int CalculateTotalState(
            PartyBattleActor partyBattleActor,
            Func<AccessoryData, StatModifier> accessoryStatSelector,
            Func<ArmorData, StatModifier> armorStatSelector,
            int baseStat)
        {
            var flatBonus = CalculateFlatBonus(partyBattleActor, accessoryStatSelector, armorStatSelector);
            var percentBonus = CalculatePercentBonus(partyBattleActor, accessoryStatSelector, armorStatSelector);

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
        
        private static int CalculateTotalStat(PartyBattleActor partyBattleActor, 
            Func<AccessoryData, StatModifier> accessoryStatSelector, 
            Func<ArmorData, StatModifier> armorStatSelector
            )
        {
            var flatBonus = CalculateFlatBonus(partyBattleActor, accessoryStatSelector, armorStatSelector);
            var percentBonus = CalculatePercentBonus(partyBattleActor, accessoryStatSelector, armorStatSelector);
            
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
        
        private static int CalculateFlatBonus(PartyBattleActor partyBattleActor,
            Func<AccessoryData, StatModifier> accessoryStatSelector,
            Func<ArmorData, StatModifier> armorStatSelector)
        {
            var flatBonus = 0;
            
            if (partyBattleActor.equipmentHandler.AccessoryData != null)
            {
                flatBonus += partyBattleActor.equipmentHandler.AccessoryData
                    .Where(accessory => accessory != null)
                    .Sum(accessory => accessoryStatSelector(accessory).modifierType == StatModifierType.Flat 
                        ? accessoryStatSelector(accessory).value 
                        : 0);
            }

            if (partyBattleActor.equipmentHandler.ArmorData != null)
            {
                var armorStat = armorStatSelector(partyBattleActor.equipmentHandler.ArmorData);
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

        private static int CalculatePercentBonus(
            PartyBattleActor partyBattleActor,
            Func<AccessoryData, StatModifier> accessoryStatSelector,
            Func<ArmorData, StatModifier> armorStatSelector)
        {
            var percentBonus = 0;
            
            if (partyBattleActor.equipmentHandler.AccessoryData != null)
            {
                percentBonus += partyBattleActor.equipmentHandler.AccessoryData
                    .Where(accessory => accessory != null)
                    .Sum(accessory => accessoryStatSelector(accessory).modifierType == StatModifierType.Percent 
                        ? accessoryStatSelector(accessory).value 
                        : 0);
            }

            if (partyBattleActor.equipmentHandler.ArmorData != null)
            {
                var armorStat = armorStatSelector(partyBattleActor.equipmentHandler.ArmorData);
                if (armorStat.modifierType == StatModifierType.Percent)
                {
                    percentBonus += armorStat.value;
                }
            }
            
            return percentBonus;
        }
    }
}

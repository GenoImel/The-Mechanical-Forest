using UnityEngine;

namespace Akashic.ScriptableObjects.Inventory
{
    [CreateAssetMenu(menuName = "Akashic/Inventory/New Armor")]
    internal sealed class ArmorData : ItemBaseData
    {
        public StatModifier might;
        public StatModifier deftness;
        public StatModifier tenacity;
        public StatModifier resolve;
        public StatModifier baseAbilityPoints;
        public StatModifier baseAbilityPointsRegen;
        public StatModifier maxHitPoints;
        
        public int bufferHitPointIncrease;
    }
}
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Inventory
{
	[CreateAssetMenu(menuName = "Akashic/Inventory/New Accessory")]
	internal sealed class AccessoryData : ItemBaseData
	{
		public StatModifier might;
		public StatModifier deftness;
		public StatModifier tenacity;
		public StatModifier resolve;
		public StatModifier baseAbilityPoints;
		public StatModifier baseAbilityPointsRegen;
		public StatModifier maxHitPoints;
		public int criticalHitPercentIncrease;
	}
}

using UnityEngine;

namespace Akashic.ScriptableObjects.Inventory
{
	[CreateAssetMenu(menuName = "Akashic/Inventory/New Consumable Item")]
	internal sealed class ConsumableData : ItemBaseData
	{
		public int pipCost;
		public bool isUsableInBattle;
		public bool isUsableInField;
	}
}

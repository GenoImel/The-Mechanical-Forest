using Akashic.Runtime.Serializers;
using Akashic.ScriptableObjects.Inventory;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEditor.PackageManager;

namespace Akashic.Runtime.Converters
{
	internal static class PartyInventoryConverter
	{
		public static PartyInventory ConvertItemDataListToPartyInventory(List<ItemBaseData> items)
		{
			List<InventoryItem> inventoryItems = new List<InventoryItem>();
			foreach (ItemBaseData itemData in items)
			{
				inventoryItems.Add(ConvertItemDataToInventoryItem(itemData));
			}
			return new PartyInventory(inventoryItems);
		}

		public static InventoryItem ConvertItemDataToInventoryItem(ItemBaseData itemData)
		{
			InventoryItem inventoryItem = null;
			switch (itemData)
			{
				case RelicData relicData:
					{
						inventoryItem = new RelicItem(
							relicData.itemId,
							1
							);
						break;
					}
				case AccessoryData accessoryData:
					{
						inventoryItem = new AccessoryItem(
							accessoryData.itemId,
							1
							);
						break;
					}
				case ConsumableData consumableData:
					{
						inventoryItem = new ConsumableItem(
							consumableData.itemId,
							1
							);
						break;
					}
			}
			return inventoryItem;
		}

	}
}

using Akashic.Runtime.Serializers;
using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;

namespace Akashic.Runtime.Converters
{
	internal static class ItemConverter
	{
		public static List<InventoryItem> ConvertInventoryDataToPartyInventory(InventoryData inventoryData)
		{
			List<InventoryItem> inventoryItems = new List<InventoryItem>();
			foreach (var keyValuePair in inventoryData.items)
			{
				var inventoryItem = ConvertItemDataToInventoryItem(keyValuePair.Key, keyValuePair.Value);
				inventoryItems.Add(inventoryItem);
			}
			return inventoryItems;
		}

		public static InventoryItem ConvertItemDataToInventoryItem(ItemBaseData itemData, int count)
		{
			InventoryItem inventoryItem = null;
			switch (itemData)
			{
				case RelicData relicData:
					{
						inventoryItem = new RelicItem(
							relicData.itemId,
							count
							);
						break;
					}
				case AccessoryData accessoryData:
					{
						inventoryItem = new AccessoryItem(
							accessoryData.itemId,
							count
							);
						break;
					}
				case ConsumableData consumableData:
					{
						inventoryItem = new ConsumableItem(
							consumableData.itemId,
							count
							);
						break;
					}
			}
			return inventoryItem;
		}

	}
}

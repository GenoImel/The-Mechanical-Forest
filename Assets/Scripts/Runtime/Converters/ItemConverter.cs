using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Inventory;
using Akashic.Runtime.Serializers.Save;

namespace Akashic.Runtime.Converters
{
	internal static class ItemConverter
	{
		public static List<InventoryItem> ConvertInventoryDataToPartyInventory(InventoryData inventoryData)
		{
			var inventoryItems = new List<InventoryItem>();
			
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
				case WeaponData weaponData:
					{
						inventoryItem = new WeaponItem(
							weaponData.itemId,
							count
							);
						break;
					}
				case ArmorData armorData:
					{
						inventoryItem = new ArmorItem(
							armorData.itemId,
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
				case RelicData relicData:
					{
						inventoryItem = new RelicItem(
							relicData.itemId,
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
				case NonConsumableData nonConsumableData:
					{
						inventoryItem = new NonConsumableItem(
							nonConsumableData.itemId,
							count
							);
						break;
					}
				case KeyItemData keyItemData:
					{
						inventoryItem = new KeyItem(
							keyItemData.itemId,
							count
							);
						break;
					}
			}
			return inventoryItem;
		}

	}
}

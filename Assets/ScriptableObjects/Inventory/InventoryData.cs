using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Akashic.ScriptableObjects.Inventory
{
	[CreateAssetMenu(menuName = "Akashic/Inventory/New Inventory")]
	internal sealed class InventoryData : ScriptableObject
	{
		/// <summary>
		/// A list of the items in the inventory, paired with their count.
		/// </summary>
		public SerializedDictionary<ItemBaseData, int> items;
	}
}

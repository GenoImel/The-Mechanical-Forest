using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic
{
	[CreateAssetMenu(menuName = "Akashic/Inventory/New Inventory")]
	internal sealed class InventoryData : ScriptableObject
	{
		/// <summary>
		/// 
		/// </summary>
		public List<ItemBaseData> items = new List<ItemBaseData>();
	}
}

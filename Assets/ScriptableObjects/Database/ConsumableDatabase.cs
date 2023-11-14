using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.ScriptableObjects.Database
{
	[CreateAssetMenu(menuName = "Akashic/Database/New Consumable Database")]
	internal sealed class ConsumableDatabase : ScriptableObject
	{
		public List<ConsumableData> consumables = new List<ConsumableData>();
	}
}

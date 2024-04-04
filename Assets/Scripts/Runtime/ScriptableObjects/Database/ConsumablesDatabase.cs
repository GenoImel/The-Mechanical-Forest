using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Inventory;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Database
{
	[CreateAssetMenu(menuName = "Akashic/Database/New Consumable Database")]
	internal sealed class ConsumablesDatabase : ScriptableObject
	{
		public List<ConsumableData> consumables = new List<ConsumableData>();
	}
}

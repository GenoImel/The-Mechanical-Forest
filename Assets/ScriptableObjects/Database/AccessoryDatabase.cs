using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.ScriptableObjects.Database
{
	[CreateAssetMenu(menuName = "Akashic/Database/New Accessory Database")]
	internal sealed class AccessoryDatabase : ScriptableObject
	{
		public List<AccessoryData> accessories = new List<AccessoryData>();
	}
}

using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Inventory;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Database
{
	[CreateAssetMenu(menuName = "Akashic/Database/New Accessory Database")]
	internal sealed class AccessoriesDatabase : ScriptableObject
	{
		public List<AccessoryData> accessories = new List<AccessoryData>();
	}
}

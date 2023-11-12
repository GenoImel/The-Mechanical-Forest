using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.ScriptableObjects.Database
{
	[CreateAssetMenu(menuName = "Akashic/Database/New Relic Database")]
	internal sealed class RelicDatabaseData : ScriptableObject
	{
		public List<RelicData> relics = new List<RelicData>();
	}
}

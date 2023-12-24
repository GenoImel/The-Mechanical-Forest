using Akashic.ScriptableObjects.Database;
using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Resource
{
	internal sealed class ResourceMonoSystem : MonoBehaviour, IResourceMonoSystem
	{
		[SerializeField] ConsumableDatabase consumablesDatabase;
		[SerializeField] AccessoryDatabase accessoriesDatabase;
		[SerializeField] RelicDatabase relicsDatabase;

		public AccessoryData GetAccessory(string itemId)
		{
			return accessoriesDatabase.accessories.First(accessory => accessory.itemId == itemId);
		}

		public List<AccessoryData> GetAccessories(List<string> itemIds)
		{
			return accessoriesDatabase.accessories.Where(accessory => itemIds.Any(it => it == accessory.itemId)).ToList();
		}

		public ConsumableData GetConsumable(string itemId)
		{
			return consumablesDatabase.consumables.First(consumable => consumable.itemId == itemId);
		}

		public List<ConsumableData> GetConsumables(List<string> itemIds)
		{
			return consumablesDatabase.consumables.Where(consumable => itemIds.Any(it => it == consumable.itemId)).ToList();
		}

		public RelicData GetRelic(string itemId)
		{
			return relicsDatabase.relics.First(relic => relic.itemId == itemId);
		}

		public List<RelicData> GetRelics(List<string> itemIds)
		{
			return relicsDatabase.relics.Where(relic => itemIds.Any(it => it == relic.itemId)).ToList();
		}
	}
}
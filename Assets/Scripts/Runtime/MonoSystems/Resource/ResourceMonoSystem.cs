using Akashic.Core;
using Akashic.ScriptableObjects.Database;
using Akashic.ScriptableObjects.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Resource
{
	internal sealed class ResourceMonoSystem : MonoBehaviour, IResourceMonoSystem
	{
		[SerializeField] ConsumableDatabaseData consumablesDatabase;
		[SerializeField] AccessoryDatabaseData accessoriesDatabase;
		[SerializeField] RelicDatabaseData relicsDatabase;

		private ICollection<ConsumableData> consumables = new List<ConsumableData>();
		private ICollection<AccessoryData> accessories = new List<AccessoryData>();
		private ICollection<RelicData> relics = new List<RelicData>();

		private void Awake()
		{
			consumables = consumablesDatabase.consumables;
			accessories = accessoriesDatabase.accessories;
			relics = relicsDatabase.relics;
		}

		public AccessoryData GetAccessory(string itemId)
		{
			return accessories.First(accessory => accessory.itemId == itemId);
		}

		public List<AccessoryData> GetAccessories(List<string> itemIds)
		{
			return accessories.Where(accessory => itemIds.Any(it => it == accessory.itemId)).ToList();
		}

		public ConsumableData GetConsumable(string itemId)
		{
			return consumables.First(consumable => consumable.itemId == itemId);
		}

		public List<ConsumableData> GetConsumables(List<string> itemIds)
		{
			return consumables.Where(consumable => itemIds.Any(it => it == consumable.itemId)).ToList();
		}

		public RelicData GetRelic(string itemId)
		{
			return relics.First(relic => relic.itemId == itemId);
		}

		public List<RelicData> GetRelics(List<string> itemIds)
		{
			return relics.Where(relic => itemIds.Any(it => it == relic.itemId)).ToList();
		}
	}
}
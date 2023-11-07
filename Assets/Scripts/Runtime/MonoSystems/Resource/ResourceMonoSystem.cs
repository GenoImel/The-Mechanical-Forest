using Akashic.Core;
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
		[SerializeField] private List<string> consumablesFilePath;
		[SerializeField] private List<string> accessoriesFilePath;
		[SerializeField] private List<string> relicsFilePath;

		private ICollection<ConsumableData> consumables = new List<ConsumableData>();
		private ICollection<AccessoryData> accessories = new List<AccessoryData>();
		private ICollection<RelicData> relics = new List<RelicData>();

		private void Awake()
		{
			Task.Run(() => LoadResourcesAsync());
		}

		private void LoadResourcesAsync()
		{
			LoadConsumables();
			LoadAccessories();
			LoadRelics();

			GameManager.Publish(new ResourcesLoadedMessage());
		}

		private IEnumerator LoadConsumables()
		{
			foreach (string consumableFilePath in consumablesFilePath)
			{
				ResourceRequest resourceRequest = Resources.LoadAsync<ConsumableData>(consumableFilePath);
				while (!resourceRequest.isDone) yield return null;
				consumables.Add(resourceRequest.asset as ConsumableData);
			}
		}

		private IEnumerator LoadAccessories()
		{
			foreach (string accessoryFilePath in accessoriesFilePath)
			{
				ResourceRequest resourceRequest = Resources.LoadAsync<AccessoryData>(accessoryFilePath);
				while (!resourceRequest.isDone) yield return null;
				accessories.Add(resourceRequest.asset as AccessoryData);
			}
		}

		private IEnumerator LoadRelics()
		{
			foreach (string relicFilePath in relicsFilePath)
			{
				ResourceRequest resourceRequest = Resources.LoadAsync<RelicData>(relicFilePath);
				while (!resourceRequest.isDone) yield return null;
				relics.Add(resourceRequest.asset as RelicData);
			}
		}

		public List<AccessoryData> GetAccessories(List<string> itemIds)
		{
			return accessories.Where(accessory => itemIds.Contains(accessory.itemId)).ToList();
		}

		public AccessoryData GetAccessory(string itemId)
		{
			return accessories.First(accessory => accessory.itemId == itemId);
		}

		public ConsumableData GetConsumable(string itemId)
		{
			return consumables.First(consumable => consumable.itemId == itemId);
		}

		public List<ConsumableData> GetConsumables(List<string> itemIds)
		{
			return consumables.Where(consumable => itemIds.Contains(consumable.itemId)).ToList();
		}

		public RelicData GetRelic(string itemId)
		{
			return relics.First(relic => relic.itemId == itemId);
		}

		public List<RelicData> GetRelics(List<string> itemIds)
		{
			return relics.Where(relic => itemIds.Contains(relic.itemId)).ToList();
		}
	}
}
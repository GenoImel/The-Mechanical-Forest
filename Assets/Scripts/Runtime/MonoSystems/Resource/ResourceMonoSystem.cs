using Akashic.Core;
using Akashic.Runtime.Converters;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.MonoSystems.Debugger;
using Akashic.Runtime.Serializers;
using Akashic.ScriptableObjects.Inventory;
using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Resource
{
	internal sealed class ResourceMonoSystem : MonoBehaviour, IResourceMonoSystem
	{
		[SerializeField] private string consumablesFilePath;
		[SerializeField] private string accessoriesFilePath;
		[SerializeField] private string relicsFilePath;

		private ICollection<ConsumableData> consumables;
		private ICollection<AccessoryData> accessories;
		private ICollection<RelicData> relics;

		private void Start()
		{
			LoadResources();
		}

		private void LoadResources()
		{
			LoadConsumablesAsync();
			LoadAccessoriesAsync();
			LoadRelicsAsync();
		}

		private IEnumerator LoadConsumablesAsync()
		{
			consumables = new List<ConsumableData>();
			foreach (string resourceToLoad in Directory.GetFiles(consumablesFilePath))
			{
				ResourceRequest resourceRequest = Resources.LoadAsync<ConsumableData>(resourceToLoad);
				yield return resourceRequest;
				consumables.Add(resourceRequest.asset as ConsumableData);
			}
		}

		private IEnumerator LoadAccessoriesAsync()
		{
			accessories = new List<AccessoryData>();
			foreach (string resourceToLoad in Directory.GetFiles(accessoriesFilePath))
			{
				ResourceRequest resourceRequest = Resources.LoadAsync<AccessoryData>(resourceToLoad);
				yield return resourceRequest;
				accessories.Add(resourceRequest.asset as AccessoryData);
			}
		}

		private IEnumerator LoadRelicsAsync()
		{
			relics = new List<RelicData>();
			foreach (string resourceToLoad in Directory.GetFiles(relicsFilePath))
			{
				ResourceRequest resourceRequest = Resources.LoadAsync<RelicData>(resourceToLoad);
				yield return resourceRequest;
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
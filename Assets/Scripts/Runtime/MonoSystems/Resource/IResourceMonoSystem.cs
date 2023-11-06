using Akashic.Core.MonoSystems;
using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;

namespace Akashic.Runtime.MonoSystems.Resource
{
	internal interface IResourceMonoSystem : IMonoSystem
	{
		public ConsumableData GetConsumable(string itemId);
		public List<ConsumableData> GetConsumables(List<string> itemId);

		public AccessoryData GetAccessory(string itemId);
		public List<AccessoryData> GetAccessories(List<string> itemId);

		public RelicData GetRelic(string itemId);
		public List<RelicData> GetRelics(List<string> itemId);
	}
}
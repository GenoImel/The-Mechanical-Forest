using Newtonsoft.Json;
using System;

namespace Akashic.Runtime.Serializers
{
	[Serializable]
	internal sealed class ConsumableItem : InventoryItem
	{
		public ConsumableItem(
			[JsonProperty("itemId")] string itemId,
			[JsonProperty("itemCount")] int itemCount
			) : base(itemId, itemCount)
		{

		}
	}
}

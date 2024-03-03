using System;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
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

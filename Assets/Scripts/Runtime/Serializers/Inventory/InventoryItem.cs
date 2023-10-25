using Newtonsoft.Json;
using System;

namespace Akashic.Runtime.Serializers
{
	[Serializable]
	internal abstract class InventoryItem
	{
		[JsonProperty("itemId")]
		public string ItemId { private set; get; }

		[JsonProperty("itemCount")]
		public int ItemCount { private set; get; }

		[JsonConstructor]
		public InventoryItem(
			[JsonProperty("itemId")] string itemId,
			[JsonProperty("itemCount")] int itemCount
			)
		{
			ItemId = itemId;
			ItemCount = itemCount;
		}
	}
}

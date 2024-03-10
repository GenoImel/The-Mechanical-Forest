using System;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
	[Serializable]
	internal abstract class InventoryItem
	{
		[JsonProperty("itemId")]
		public string ItemId { private set; get; }

		[JsonProperty("itemCount")]
		public int ItemCount { private set; get; }

		[JsonConstructor]
		protected InventoryItem(
			[JsonProperty("itemId")] string itemId,
			[JsonProperty("itemCount")] int itemCount
			)
		{
			ItemId = itemId;
			ItemCount = itemCount;
		}
	}
}

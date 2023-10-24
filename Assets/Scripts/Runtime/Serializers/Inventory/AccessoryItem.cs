using Newtonsoft.Json;
using System;

namespace Akashic.Runtime.Serializers
{
	[Serializable]
	internal sealed class AccessoryItem : InventoryItem
	{
		public AccessoryItem(
			[JsonProperty("itemId")] string itemId,
			[JsonProperty("itemCount")] int itemCount
			) : base(itemId, itemCount)
		{

		}
	}
}

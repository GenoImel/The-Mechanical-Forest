using System;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
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

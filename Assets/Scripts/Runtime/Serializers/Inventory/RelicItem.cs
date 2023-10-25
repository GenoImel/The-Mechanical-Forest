using Newtonsoft.Json;
using System;

namespace Akashic.Runtime.Serializers
{
	[Serializable]
	internal sealed class RelicItem : InventoryItem
	{
		public RelicItem(
			[JsonProperty("itemId")] string itemId, 
			[JsonProperty("itemCount")] int itemCount
			) : base(itemId, itemCount)
		{

		}
	}
}

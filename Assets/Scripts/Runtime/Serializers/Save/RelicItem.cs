using Newtonsoft.Json;
using System;
using Akashic.Runtime.Serializers.Save;

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

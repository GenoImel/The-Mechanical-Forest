using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers
{
	[Serializable]
	internal sealed class PartyInventory
	{
		[JsonProperty("items")]
		public List<InventoryItem> Items { private set; get; }

		[JsonIgnore]
		public List<RelicItem> Relics => Items.OfType<RelicItem>().ToList();

		[JsonIgnore]
		public List<ConsumableItem> Consumables => Items.OfType<ConsumableItem>().ToList();

		[JsonIgnore]
		public List<AccessoryItem> Accessories => Items.OfType<AccessoryItem>().ToList();

		[JsonConstructor]
		public PartyInventory(
			[JsonProperty("items")] List<InventoryItem> items
			)
		{
			Items = items;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
	[Serializable]
	internal sealed class PartyInventory
	{
		[JsonProperty("items")]
		public List<InventoryItem> Items { private set; get; }
		
		[JsonIgnore]
		public List<WeaponItem> Weapons => Items.OfType<WeaponItem>().ToList();
		
		[JsonIgnore]
		public List<ArmorItem> Armors => Items.OfType<ArmorItem>().ToList();
		
		[JsonIgnore]
		public List<AccessoryItem> Accessories => Items.OfType<AccessoryItem>().ToList();

		[JsonIgnore]
		public List<RelicItem> Relics => Items.OfType<RelicItem>().ToList();

		[JsonIgnore]
		public List<ConsumableItem> Consumables => Items.OfType<ConsumableItem>().ToList();
		
		[JsonIgnore]
		public List<NonConsumableItem> NonConsumables => Items.OfType<NonConsumableItem>().ToList();
		
		[JsonIgnore]
		public List<KeyItem> KeyItems => Items.OfType<KeyItem>().ToList();

		[JsonConstructor]
		public PartyInventory(
			[JsonProperty("items")] List<InventoryItem> items
			)
		{
			Items = items;
		}
	}
}
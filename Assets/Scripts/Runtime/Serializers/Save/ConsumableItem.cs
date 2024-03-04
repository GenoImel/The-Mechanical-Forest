﻿using System;
using Newtonsoft.Json;

namespace Akashic.Runtime.Serializers.Save
{
	[Serializable]
	internal sealed class ConsumableItem : InventoryItem
	{
		public ConsumableItem(string itemId, int itemCount) : base(itemId, itemCount)
		{

		}
	}
}

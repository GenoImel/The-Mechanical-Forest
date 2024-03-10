using System;

namespace Akashic.Runtime.Serializers.Save
{
	[Serializable]
	internal sealed class AccessoryItem : InventoryItem
	{
		public AccessoryItem(string itemId, int itemCount) : base(itemId, itemCount)
		{

		}
	}
}

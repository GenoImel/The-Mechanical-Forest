using System;

namespace Akashic.Runtime.Serializers.Save
{
	[Serializable]
	internal sealed class RelicItem : InventoryItem
	{
		public RelicItem(string itemId, int itemCount) : base(itemId, itemCount)
		{

		}
	}
}

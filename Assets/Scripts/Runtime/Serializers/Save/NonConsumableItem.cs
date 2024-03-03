using System;

namespace Akashic.Runtime.Serializers.Save
{
    [Serializable]
    internal sealed class NonConsumableItem : InventoryItem
    {
        public NonConsumableItem(
            string itemId,
            int itemCount
            ) : base(itemId, itemCount)
        {
            
        }
    }
}
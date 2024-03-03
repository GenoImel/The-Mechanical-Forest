using System;

namespace Akashic.Runtime.Serializers.Save
{
    [Serializable]
    internal sealed class KeyItem : InventoryItem
    {
        public KeyItem(
            string itemId,
            int itemCount
            ) : base(itemId, itemCount)
        {
            
        }
    }
}
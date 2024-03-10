using System;

namespace Akashic.Runtime.Serializers.Save
{
    [Serializable]
    internal sealed class ArmorItem : InventoryItem
    {
        public ArmorItem(string itemId, int itemCount) : base(itemId, itemCount)
        {
            
        }
    }
}
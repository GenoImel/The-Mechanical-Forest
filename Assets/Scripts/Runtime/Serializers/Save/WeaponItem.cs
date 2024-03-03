using System;

namespace Akashic.Runtime.Serializers.Save
{
    [Serializable]
    internal sealed class WeaponItem : InventoryItem
    {
        public WeaponItem(
            string itemId,
            int itemCount
            ) : base(itemId, itemCount)
        {

        }
    }
}
using UnityEngine;

namespace Akashic.ScriptableObjects.Inventory
{
    [CreateAssetMenu(menuName = "Akashic/Inventory/New Key Item")]
    internal sealed class KeyItemData : ItemBaseData
    {
        public bool isDestroyedAfterUse;
    }
}
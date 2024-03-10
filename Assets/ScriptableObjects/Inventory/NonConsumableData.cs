using UnityEngine;

namespace Akashic.ScriptableObjects.Inventory
{
    [CreateAssetMenu(menuName = "Akashic/Inventory/New NonConsumable Item")]
    internal sealed class NonConsumableData : ItemBaseData
    {
        public int pipCost;
        public bool isUsableInBattle;
        public bool isUsableInField;
        public bool isUsableOnlyOnce;
    }
}
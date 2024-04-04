using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Inventory
{
    [CreateAssetMenu(menuName = "Akashic/Inventory/New Weapon")]
    internal sealed class WeaponData : ItemBaseData
    {
        public int attackDamageIncrease;
        public int magicDamageIncrease;
    }
}
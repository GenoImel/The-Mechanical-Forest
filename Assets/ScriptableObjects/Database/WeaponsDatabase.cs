using System.Collections.Generic;
using Akashic.ScriptableObjects.Inventory;
using UnityEngine;

namespace Akashic.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Weapon Database")]
    internal sealed class WeaponsDatabase : ScriptableObject
    {
        public List<WeaponData> weapons = new List<WeaponData>();
    }
}
using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Inventory;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Weapon Database")]
    internal sealed class WeaponsDatabase : ScriptableObject
    {
        public List<WeaponData> weapons = new List<WeaponData>();
    }
}
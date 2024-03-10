using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Armor Database")]
    internal sealed class ArmorDatabase : ScriptableObject
    {
        public List<ArmorData> armors = new List<ArmorData>();
    }
}
using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Inventory;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Armor Database")]
    internal sealed class ArmorDatabase : ScriptableObject
    {
        public List<ArmorData> armors = new List<ArmorData>();
    }
}
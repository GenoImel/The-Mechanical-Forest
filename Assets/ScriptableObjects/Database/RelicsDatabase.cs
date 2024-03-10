using System.Collections.Generic;
using Akashic.ScriptableObjects.Inventory;
using UnityEngine;

namespace Akashic.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Relics Database")]
    internal sealed class RelicsDatabase : ScriptableObject
    {
        public List<RelicData> relics = new List<RelicData>();
    }
}
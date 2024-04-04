using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Inventory;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Relics Database")]
    internal sealed class RelicsDatabase : ScriptableObject
    {
        public List<RelicData> relics = new List<RelicData>();
    }
}
using System.Collections.Generic;
using Akashic.ScriptableObjects.Inventory;
using UnityEngine;

namespace Akashic.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Non-Consumables Database")]
    internal sealed class NonConsumablesDatabase : ScriptableObject
    {
        public List<NonConsumableData> nonConsumables = new List<NonConsumableData>();
    }
}
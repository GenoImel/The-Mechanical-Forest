using System.Collections.Generic;
using Akashic.ScriptableObjects.Battle;
using UnityEngine;

namespace Akashic.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Enemy Database")]
    internal sealed class EnemyDatabase : ScriptableObject
    {
        public List<EnemyData> enemies = new List<EnemyData>();
    }
}
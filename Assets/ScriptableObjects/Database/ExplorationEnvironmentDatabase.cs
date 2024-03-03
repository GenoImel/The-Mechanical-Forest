using System.Collections.Generic;
using Akashic.ScriptableObjects.Exploration;
using UnityEngine;

namespace Akashic.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Environment Database")]
    internal sealed class ExplorationEnvironmentDatabase : ScriptableObject
    {
        public List<ExplorationEnvironmentData> environments;
    }
}
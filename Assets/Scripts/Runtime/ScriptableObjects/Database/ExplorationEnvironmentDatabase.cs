using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Exploration;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Environment Database")]
    internal sealed class ExplorationEnvironmentDatabase : ScriptableObject
    {
        public List<ExplorationEnvironmentData> environments;
    }
}
using System.Collections.Generic;
using Akashic.Runtime.ScriptableObjects.Battle;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Database
{
    [CreateAssetMenu(menuName = "Akashic/Database/New Encounter Database")]
    internal sealed class EncounterDatabase : ScriptableObject
    {
        public List<EncounterData> encounters = new List<EncounterData>();
    }
}
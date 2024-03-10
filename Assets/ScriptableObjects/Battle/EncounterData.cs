using System.Collections.Generic;
using UnityEngine;

namespace Akashic.ScriptableObjects.Battle
{
    [CreateAssetMenu(menuName = "Akashic/Battle/New Encounter Data")]
    internal sealed class EncounterData : ScriptableObject
    {
        public string encounterName;
        public string encounterId;
        public string encounterDescription;
        public int scaledEncounterLevel;
        public List<EnemyQuantityRange> enemiesAndQuantities;
    }
}
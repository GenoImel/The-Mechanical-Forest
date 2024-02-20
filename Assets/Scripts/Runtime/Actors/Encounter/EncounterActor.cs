using Akashic.Runtime.Actors.Battle;
using Akashic.ScriptableObjects.Battle;
using UnityEngine;

namespace Akashic.Runtime.Actors.Encounter
{
    internal sealed class EncounterActor : MonoBehaviour
    {
        [SerializeField] private EncounterData encounterData;
        [SerializeField] private BattleEnvironment battleEnvironment;
    }
}
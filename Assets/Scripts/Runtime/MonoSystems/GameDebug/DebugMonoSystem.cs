using System.Collections.Generic;
using Akashic.Runtime.Actors.Battle.Environment;
using Akashic.Runtime.ScriptableObjects.Battle;
using Akashic.Runtime.ScriptableObjects.Exploration;
using Akashic.Runtime.ScriptableObjects.Inventory;
using Akashic.Runtime.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.GameDebug
{
    internal sealed class DebugMonoSystem : MonoBehaviour, IDebugMonoSystem
    {
        [SerializeField] private PartyData debugPartyData;

        [SerializeField] private InventoryData debugInventoryData;

        [SerializeField] private ExplorationEnvironmentData debugExplorationEnvironment;
        
        [SerializeField] private EncounterData debugEncounterData;
        
        [SerializeField] private BattleEnvironment debugBattleEnvironment;

        [SerializeField] private bool isDebugMode = false;
        
        public bool IsDebugMode => isDebugMode;

        public List<PartyMemberData> GetDebugParty()
        {
            return debugPartyData.partyMembers;
        }

        public InventoryData GetDebugInventory()
        {
            return debugInventoryData;
        }
        
        public ExplorationEnvironmentData GetDebugExplorationEnvironment()
        {
            return debugExplorationEnvironment;
        }
        
        public EncounterData GetDebugEncounterData()
        {
            return debugEncounterData;
        }
        
        public BattleEnvironment GetDebugBattleEnvironment()
        {
            return debugBattleEnvironment;
        }
    }
}

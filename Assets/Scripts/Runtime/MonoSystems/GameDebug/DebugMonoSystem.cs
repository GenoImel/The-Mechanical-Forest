using System.Collections.Generic;
using Akashic.Runtime.Actors.Battle;
using Akashic.ScriptableObjects.Battle;
using Akashic.ScriptableObjects.Exploration;
using Akashic.ScriptableObjects.Inventory;
using Akashic.ScriptableObjects.PartyMember;
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

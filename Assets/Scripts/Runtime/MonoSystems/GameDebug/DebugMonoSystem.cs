using System.Collections.Generic;
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

        [SerializeField] private ExplorationEnvironmentData debugEnvironment;

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
        
        public ExplorationEnvironmentData GetDebugEnvironment()
        {
            return debugEnvironment;
        }
    }
}

using System.Collections.Generic;
using Akashic.Core.MonoSystems;
using Akashic.Runtime.Actors.Battle;
using Akashic.ScriptableObjects.Battle;
using Akashic.ScriptableObjects.Exploration;
using Akashic.ScriptableObjects.Inventory;
using Akashic.ScriptableObjects.PartyMember;

namespace Akashic.Runtime.MonoSystems.GameDebug
{
    internal interface IDebugMonoSystem : IMonoSystem
    {
        /// <summary>
        /// States whether or not debug mode is enabled for the game.
        /// </summary>
        public bool IsDebugMode { get; }
        
        /// <returns>A list of debug <see cref="PartyMemberData"/>.</returns>
        public List<PartyMemberData> GetDebugParty();
        
        /// <returns>Returns the debug <see cref="InventoryData"/>.</returns>
        public InventoryData GetDebugInventory();
        
        /// <returns>Returns the debug <see cref="ExplorationEnvironmentData"/>.</returns>
        public ExplorationEnvironmentData GetDebugExplorationEnvironment();

        /// <returns>Returns the debug <see cref="EncounterData"/>.</returns>
        public EncounterData GetDebugEncounterData();
        
        /// <returns>Returns the debug <see cref="BattleEnvironment"/>.</returns>
        public BattleEnvironment GetDebugBattleEnvironment();
    }
}
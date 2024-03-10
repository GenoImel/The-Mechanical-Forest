using System.Collections.Generic;
using Akashic.Core.MonoSystems;
using Akashic.Runtime.MonoSystems.Config.Settings;
using Akashic.ScriptableObjects.Config;
using Akashic.ScriptableObjects.Inventory;
using Akashic.ScriptableObjects.PartyMember;

namespace Akashic.Runtime.MonoSystems.Config
{
    /// <summary>
    /// Provides access to configuration data for the game, which is set in a
    /// ConfigBaseData ScriptableObject.
    /// </summary>
    internal interface IConfigMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Returns the configuration settings for saving and loading game data.
        /// </summary>
        /// <returns><see cref="SaveConfigSettings"/></returns>
        public SaveConfigSettings GetSaveConfigSettings();
		
        /// <summary>
        /// Returns the default party members for a new game.
        /// </summary>
        /// <returns><see cref="PartyMemberData"/></returns>
        public List<PartyMemberData> GetDefaultParty();
		
        /// <summary>
        /// Returns the default inventory for a new game.
        /// </summary>
        /// <returns><see cref="InventoryData"/></returns>
        public InventoryData GetDefaultInventory();

        /// <summary>
        /// Returns the room ID for the starting exploration scene.
        /// </summary>
        public string GetRoomId();

        /// <summary>
        /// Returns the spawn point ID for the starting exploration scene.
        /// </summary>
        public string GetSpawnPointId();

        /// <summary>
        /// Returns the base battle settings for the game.
        /// </summary>
        /// <returns><see cref="GameConfigData"/></returns>
        public GameConfigData GetBattleConfigData();
    }
}
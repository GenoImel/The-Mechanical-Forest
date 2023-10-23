using System.Collections.Generic;
using System.Threading.Tasks;
using Akashic.Core;
using Akashic.Core.MonoSystems;
using Akashic.Runtime.Serializers;

namespace Akashic.Runtime.MonoSystems.Save
{
    internal interface ISaveMonoSystem : IMonoSystem
    {
        /// <summary>
        /// See if a save file exists.
        /// If it does, return the player defined save file name.
        /// </summary>
        public Task<string> FindSaveFile(string saveSlotFileName);

		/// <summary>
		/// Returns the current party members as a list.
		/// </summary>
		public List<PartyMember> GetPartyMembers();

		/// <summary>
		/// Returns the current inventory.
		/// </summary>
		public PartyInventory GetPartyInventory();

		/// <summary>
		/// Saves new data to the currently active save file.
		/// </summary>
		public void SaveFileAsync();
        
        /// <summary>
        /// Loads data from a selected save file.
        /// </summary>
        public void LoadFileAsync();

        /// <summary>
        /// Initializes a new save file.
        /// </summary>
        public void InitializeNewFile(SaveFile saveFile, string saveSlotFileName);
    }
}
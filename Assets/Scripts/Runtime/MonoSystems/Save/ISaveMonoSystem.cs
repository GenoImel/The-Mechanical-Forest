using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.Serializers;

namespace Akashic.Runtime.MonoSystems.Save
{
    internal interface ISaveMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Returns the current party members as a list.
        /// </summary>
        public List<PartyMember> GetPartyMembers();

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
        public void InitializeNewFile();
    }
}
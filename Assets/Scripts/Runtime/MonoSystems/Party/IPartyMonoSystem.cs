using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.Controllers.PartyMemberBattle;

namespace Akashic.Runtime.MonoSystems.Party
{
    internal interface IPartyMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Creates a new party using default values
        /// specified by the scriptable objects.
        /// </summary>
        public void CreateNewParty();
        
        /// <summary>
        /// Returns a list of the party members.
        /// </summary>
        public List<PartyMemberController> GetPartyMembers();
    }
}
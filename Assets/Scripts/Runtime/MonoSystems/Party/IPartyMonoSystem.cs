using System.Collections.Generic;
using Akashic.Core.MonoSystems;
using Akashic.Runtime.Serializers.Save;

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
		/// Returns a list of <see cref="PartyMember"/>.
		/// </summary>
		public List<PartyMember> GetPartyMembers();
    }
}
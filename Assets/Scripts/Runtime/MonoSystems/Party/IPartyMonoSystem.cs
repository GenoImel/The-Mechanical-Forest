using Akashic.Core;

namespace Akashic.Runtime.MonoSystems.Party
{
    internal interface IPartyMonoSystem : IMonoSystem
    {
        /// <summary>
        /// Creates a new party using default values
        /// specified by the scriptable objects.
        /// </summary>
        public void CreateNewParty();
    }
}
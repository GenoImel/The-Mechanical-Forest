using Akashic.Core;
using Akashic.Core.MonoSystems;

namespace Akashic.Runtime.MonoSystems.ExplorationStates
{
    internal interface IExplorationStateMonoSystem : IMonoSystem
    {
        public void SetInitializingExplorationState();
        public void SetExplorationExplorationState();
        public void SetPartyExplorationState();
        public void SetInventoryExplorationState();
        public void SetNoneExplorationState();
    }
}

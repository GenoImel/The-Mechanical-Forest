using Akashic.Core;

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

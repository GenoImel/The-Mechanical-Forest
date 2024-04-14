using System.Collections.Generic;
using Akashic.Core.MonoSystems;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Party;

namespace Akashic.Runtime.MonoSystems.Battle
{
    internal interface IPartyBattleMonoSystem : IMonoSystem
    {
        public void AddPartyBattleActor(PartyBattleActor partyBattleActor);

        public void InitializeAbilityPoints();

        public List<BattleActor> GetBattleActorsAsBase();

        public List<PartyBattleActor> GetBattleActors();
    }
}
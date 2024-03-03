using System.Collections.Generic;
using Akashic.Runtime.Actors.Battle;
using Akashic.Runtime.Utilities.GameMath.Resources;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.PartyResources
{
    internal sealed class PartyBattleMonoSystem : IPartyBattleMonoSystem
    {
        [Header("Battle Actors")]
        [SerializeField] private List<PartyBattleActor> partyBattleActors;
        
        private int currentAbilityPoints;
        private int maxAbilityPoints;
        
        public int CurrentAbilityPoints => currentAbilityPoints;

        public void AddPartyBattleActor(PartyBattleActor partyBattleActor)
        {
            partyBattleActors.Add(partyBattleActor);
        }

        public void InitializeAbilityPoints()
        {
            maxAbilityPoints = ResourcesMath.CalculateTotalPooledAbilityPoints(partyBattleActors);
            currentAbilityPoints = maxAbilityPoints;
        }
    }
}
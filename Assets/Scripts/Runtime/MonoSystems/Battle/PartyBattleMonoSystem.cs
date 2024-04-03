using System.Collections.Generic;
using System.Linq;
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Party;
using Akashic.Runtime.Utilities.GameMath.Resources;
using UnityEngine;

namespace Akashic.Runtime.MonoSystems.Battle
{
    internal sealed class PartyBattleMonoSystem : MonoBehaviour, IPartyBattleMonoSystem
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
        
        public List<BattleActor> GetBattleActors()
        {
            return partyBattleActors.Cast<BattleActor>().ToList();
        }
    }
}
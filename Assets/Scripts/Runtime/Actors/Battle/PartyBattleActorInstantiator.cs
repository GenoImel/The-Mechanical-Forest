using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.MonoSystems.Party;
using Akashic.Runtime.MonoSystems.PartyResources;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class PartyBattleActorInstantiator : MonoBehaviour
    {
        [Header("Party Battle Actors")]
        [SerializeField] private List<PartyBattleActor> partyBattleActors;
        
        [Header("Instantiation")]
        [SerializeField] private List<Transform> partyBattleActorSpawnPoints;
        
        private IPartyMonoSystem partyMonoSystem;
        private IPartyBattleMonoSystem partyBattleMonoSystem;
        
        private void Awake()
        {
            partyMonoSystem = GameManager.GetMonoSystem<IPartyMonoSystem>();
            partyBattleMonoSystem = GameManager.GetMonoSystem<IPartyBattleMonoSystem>();
        }

        public void InstantiatePartyBattleActors()
        {
            var currentPartyMembers = partyMonoSystem.GetPartyMembers();
            
            for (var i = 0; i < currentPartyMembers.Count; i++)
            {
                var partyMember = currentPartyMembers[i];
                var partyBattleActor = partyBattleActors
                    .Find(x => x.PartyMemberName == partyMember.PartyMemberName);

                var instantiatedPartyBattleActor = Instantiate(partyBattleActor, transform);

                instantiatedPartyBattleActor.transform.position = partyBattleActorSpawnPoints[i].position;
                partyBattleActor.InitializePartyBattleActor(partyMember);
                partyBattleMonoSystem.AddPartyBattleActor(instantiatedPartyBattleActor);
            }
            
            partyBattleMonoSystem.InitializeAbilityPoints();
        }
    }
}
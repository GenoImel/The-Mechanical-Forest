using System;
using System.Collections.Generic;
using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Party;
using UnityEngine;

namespace Akashic.Runtime.Controllers.BattleHud
{
    internal sealed class PartyBattleProfileContainer : MonoBehaviour
    {
        [SerializeField] private List<PartyMemberBattleProfile> partyMemberBattleProfiles;

        [SerializeField] private PartyMemberBattleProfile battleProfilePrefab;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void OnPartyBattleActorAddedMessage(PartyBattleActorAddedMessage message)
        {
            var instantiatedBattleProfile = Instantiate(battleProfilePrefab, transform);
            instantiatedBattleProfile.InitializeBattleProfile(message.partyBattleActor);
        }

        private void AddListeners()
        {
            GameManager.AddListener<PartyBattleActorAddedMessage>(OnPartyBattleActorAddedMessage);
        }

        private void RemoveListeners()
        {
            GameManager.RemoveListener<PartyBattleActorAddedMessage>(OnPartyBattleActorAddedMessage);
        }
    }
}
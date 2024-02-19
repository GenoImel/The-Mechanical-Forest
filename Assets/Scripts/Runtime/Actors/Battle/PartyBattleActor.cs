using Akashic.Runtime.Serializers.Party;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class PartyBattleActor : MonoBehaviour
    {
        [Header("Party Member Info")]
        [SerializeField] private string partyMemberName;

        [Header("Handlers")]
        [SerializeField] public PartyMemberStatHandler partyMemberStatHandler;

        [SerializeField] public PartyMemberResourceHandler partyMemberResourceHandler;

        [SerializeField] public PartyMemberSkillsHandler partyMemberSkillsHandler;

        [SerializeField] public PartyMemberAnimationHandler partyMemberAnimationHandler;

        [SerializeField] public PartyMemberEffectHandler partyMemberEffectHandler;

        [SerializeField] public PartyMemberSoundHandler partyMemberSoundHandler;

        public string PartyMemberName => partyMemberName;

        public void InitializePartyMemberFromSaveData(PartyMember partyMember)
        {
            //partyMemberStatHandler.InitializePartyMemberFromSaveData();
            //partyMemberResourceHandler.InitializePartyMemberFromSaveData();
        }
    }
}
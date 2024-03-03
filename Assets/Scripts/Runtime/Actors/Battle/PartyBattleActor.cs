using Akashic.Runtime.Serializers.Save;
using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class PartyBattleActor : MonoBehaviour
    {
        [Header("Party Member Info")]
        [SerializeField] private string partyMemberName;

        [Header("Handlers")]
        [SerializeField] public PartyBattleActorStatHandler statHandler;

        [SerializeField] public PartyMemberSkillsHandler partyMemberSkillsHandler;

        [SerializeField] public PartyMemberAnimationHandler partyMemberAnimationHandler;

        [SerializeField] public PartyMemberEffectHandler partyMemberEffectHandler;

        [SerializeField] public PartyMemberSoundHandler partyMemberSoundHandler;
        
        public PartyMemberData partyMemberData;

        public string PartyMemberName => partyMemberName;

        public void InitializePartyBattleActor(PartyMember partyMember)
        {
            statHandler.InitializePartyBattleActorStats(partyMember);
        }
    }
}
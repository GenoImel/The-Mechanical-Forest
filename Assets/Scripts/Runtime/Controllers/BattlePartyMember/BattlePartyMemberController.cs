using System.Collections.Generic;
using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.Controllers.BattlePartyMember
{
    internal sealed class BattlePartyMemberController : MonoBehaviour
    {
        [Header("Party Member Info")]
        [SerializeField] private string partyMemberName;

        [Header("Base Data")]
        [SerializeField] private PartyMemberData partyMemberData;
        
        [SerializeField] private List<SkillData> skillsData;
        
        [Header("Handlers")]
        [SerializeField] public PartyMemberStatHandler partyMemberStatHandler;

        [SerializeField] public PartyMemberResourceHandler partyMemberResourceHandler;

        [SerializeField] public PartyMemberSkillsHandler partyMemberSkillsHandler;

        [SerializeField] public PartyMemberAnimationHandler partyMemberAnimationHandler;

        [SerializeField] public PartyMemberEffectHandler partyMemberEffectHandler;

        [SerializeField] public PartyMemberSoundHandler partyMemberSoundHandler;
        
        public string PartyMemberName => partyMemberName;

        public void InitializeNewPartyMemberFromScriptableObject()
        {
            partyMemberName = partyMemberData.partyMemberName;
            
            partyMemberStatHandler.InitializeNewPartyMemberFromScriptableObject(partyMemberData);
            partyMemberResourceHandler.InitializeNewPartyMemberFromScriptableObject(partyMemberData);
        }
    }
}
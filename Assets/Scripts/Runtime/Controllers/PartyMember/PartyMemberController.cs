using System.Collections.Generic;
using Akashic.ScriptableObjects.Scripts;
using UnityEngine;

namespace Akashic.Runtime.Controllers.PartyMember
{
    internal sealed class PartyMemberController : MonoBehaviour
    {
        [Header("Party Member Info")]
        [SerializeField] private string partyMemberName;
        
        [Header("Base Data")]
        [SerializeField] private PartyMemberBaseData partyMemberBaseData;
        
        [SerializeField] private List<SkillBaseData> skillsBaseData;
        
        [Header("Handlers")]
        [SerializeField] private PartyMemberStatHandler partyMemberStatHandler;

        [SerializeField] private PartyMemberResourceHandler partyMemberResourceHandler;

        [SerializeField] private PartyMemberSkillsHandler partyMemberSkillsHandler;

        [SerializeField] private PartyMemberAnimationHandler partyMemberAnimationHandler;

        [SerializeField] private PartyMemberEffectHandler partyMemberEffectHandler;

        [SerializeField] private PartyMemberSoundHandler partyMemberSoundHandler;

        public void InitializeNewPartyMemberFromScriptableObject()
        {
            partyMemberName = partyMemberBaseData.partyMemberName;
            
            partyMemberStatHandler.InitializeNewPartyMemberFromScriptableObject(partyMemberBaseData);
            partyMemberResourceHandler.InitializeNewPartyMemberFromScriptableObject(partyMemberBaseData);
        }
    }
}
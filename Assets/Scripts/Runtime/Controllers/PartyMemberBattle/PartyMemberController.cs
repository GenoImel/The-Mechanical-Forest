using System.Collections.Generic;
using Akashic.ScriptableObjects.PartyMemberBase;
using UnityEngine;

namespace Akashic.Runtime.Controllers.PartyMemberBattle
{
    internal sealed class PartyMemberController : MonoBehaviour
    {
        [Header("Party Member Info")]
        [SerializeField] private string partyMemberName;

        [Header("Base Data")]
        [SerializeField] private PartyMemberBaseData partyMemberBaseData;
        
        [SerializeField] private List<SkillBaseData> skillsBaseData;
        
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
            partyMemberName = partyMemberBaseData.partyMemberName;
            
            partyMemberStatHandler.InitializeNewPartyMemberFromScriptableObject(partyMemberBaseData);
            partyMemberResourceHandler.InitializeNewPartyMemberFromScriptableObject(partyMemberBaseData);
        }
    }
}
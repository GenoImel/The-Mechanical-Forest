using Akashic.ScriptableObjects.PartyMemberBase;
using UnityEngine;

namespace Akashic.Runtime.Controllers.PartyMemberBattle
{
    internal sealed class PartyMemberResourceHandler : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private PartyMemberController partyMemberController;
        
        [Header("Resources")]
        [SerializeField] public int currentHealth;

        [SerializeField] public int currentExperience;

        public int maxHealth;
        public int maxExperience;
        
        public void InitializeNewPartyMemberFromScriptableObject(PartyMemberBaseData baseData)
        {
            currentHealth = baseData.baseHealth;
            maxHealth = baseData.baseHealth;

            currentExperience = baseData.baseExp;
        }
    }
}
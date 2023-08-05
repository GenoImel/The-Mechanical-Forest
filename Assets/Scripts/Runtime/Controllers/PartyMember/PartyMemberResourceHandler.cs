using Akashic.ScriptableObjects.Scripts;
using UnityEngine;

namespace Akashic.Runtime.Controllers.PartyMember
{
    internal sealed class PartyMemberResourceHandler : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private PartyMemberController partyMemberController;
        
        [Header("Resources")]
        [SerializeField] public int currentHealth;

        [SerializeField] public int currentExperience;

        private int maxHealth;
        private int maxExperience;
        
        public void InitializeNewPartyMemberFromScriptableObject(PartyMemberBaseData baseData)
        {
            currentHealth = baseData.baseHealth;
            maxHealth = baseData.baseHealth;

            currentExperience = baseData.baseExp;
        }
    }
}
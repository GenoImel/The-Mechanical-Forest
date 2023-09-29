using Akashic.ScriptableObjects.Scripts;
using Akashic.ScriptableObjects.Scripts.PartyMemberBase;
using UnityEngine;

namespace Akashic.Runtime.Controllers.PartyMemberBattle
{
    internal sealed class PartyMemberResourceHandler : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private PartyMemberController partyMemberController;
        
        [Header("Resources")]
        [SerializeField] private int currentHealth;

        [SerializeField] private int currentExperience;

        private int maxHealth;
        private int maxExperience;
        
        public int CurrentHealth => currentHealth;
        public int CurrentExperience => currentExperience;

        public int MaxHealth => maxHealth;
        public int MaxExperience => maxExperience;

        public void InitializeNewPartyMemberFromScriptableObject(PartyMemberBaseData baseData)
        {
            currentHealth = baseData.baseHealth;
            currentExperience = baseData.baseExp;
            
            maxHealth = baseData.baseHealth;
        }
    }
}
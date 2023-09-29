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
        
        public int CurrentHealth { get => currentHealth; private set => currentHealth = value; }
        public int CurrentExperience { get => currentExperience; private set => currentExperience = value; }
        
        public int MaxHealth { get => maxHealth; }
        public int MaxExperience { get => maxExperience; }
        
        public void InitializeNewPartyMemberFromScriptableObject(PartyMemberBaseData baseData)
        {
            currentHealth = baseData.baseHealth;
            maxHealth = baseData.baseHealth;

            currentExperience = baseData.baseExp;
        }
    }
}
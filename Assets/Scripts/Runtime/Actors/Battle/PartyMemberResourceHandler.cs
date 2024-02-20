using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class PartyMemberResourceHandler : MonoBehaviour
    {
        [Header("Resources")]
        [SerializeField] private int currentHealth;

        [SerializeField] private int currentExperience;

        private int maxHealth;
        private int maxExperience;
        
        public int CurrentHealth => currentHealth;
        public int CurrentExperience => currentExperience;

        public int MaxHealth => maxHealth;
        public int MaxExperience => maxExperience;

        /*public void InitializeNewPartyMemberFromScriptableObject(PartyMemberData partymemberData)
        {
            currentHealth = partymemberData.baseHealth;
            currentExperience = partymemberData.baseExp;
            
            maxHealth = partymemberData.baseHealth;
        }*/
    }
}
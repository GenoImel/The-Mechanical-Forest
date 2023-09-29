using Akashic.ScriptableObjects.PartyMemberBase;
using UnityEngine;

namespace Akashic.Runtime.Controllers.PartyMemberBattle
{
    internal sealed class PartyMemberStatHandler : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private PartyMemberController partyMemberController;
        
        [Header("Stats")]
        [SerializeField] public int currentLevel;
        
        [SerializeField] public int currentPhysicalAttack;
        [SerializeField] public int currentMagicalAttack;
        [SerializeField] public float currentAccuracy;
        
        [SerializeField] public int currentPhysicalDefense;
        [SerializeField] public int currentMagicalDefense;
        [SerializeField] public float currentEvade;
        
        public int basePhysicalAttack;
        public int baseMagicalAttack;
        public float baseAccuracy;
        
        public int basePhysicalDefense;
        public int baseMagicalDefense;
        public float baseEvade;
        
        public void InitializeNewPartyMemberFromScriptableObject(PartyMemberBaseData baseData)
        {
            basePhysicalAttack = baseData.basePhysicalAttack;
            baseMagicalAttack = baseData.baseMagicalAttack;
            baseAccuracy = baseData.baseAccuracy;

            basePhysicalDefense = baseData.basePhysicalDefense;
            baseMagicalDefense = baseData.baseMagicalDefense;
            baseEvade = baseData.baseEvade;
            
            CalculateCurrentStats();
        }

        private void CalculateCurrentStats()
        {
            
        }
    }
}
using Akashic.ScriptableObjects.PartyMemberBase;
using UnityEngine;

namespace Akashic.Runtime.Controllers.PartyMemberBattle
{
    internal sealed class PartyMemberStatHandler : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private PartyMemberController partyMemberController;
        
        [Header("Stats")]
        [SerializeField] private int currentLevel;
        
        [SerializeField] private int currentPhysicalAttack;
        [SerializeField] private int currentMagicalAttack;
        [SerializeField] private float currentAccuracy;
        
        [SerializeField] private int currentPhysicalDefense;
        [SerializeField] private int currentMagicalDefense;
        [SerializeField] private float currentEvade;
        
        [SerializeField] private int basePhysicalAttack;
        [SerializeField] private int baseMagicalAttack;
        [SerializeField] private float baseAccuracy;
        
        [SerializeField] private int basePhysicalDefense;
        [SerializeField] private int baseMagicalDefense;
        [SerializeField] private float baseEvade;
        
        public int CurrentLevel => currentLevel;
        public int CurrentPhysicalAttack => currentPhysicalAttack;
        public int CurrentMagicalAttack => currentMagicalAttack;
        public float CurrentAccuracy => currentAccuracy;

        public int CurrentPhysicalDefense => currentPhysicalDefense;
        public int CurrentMagicalDefense => currentMagicalDefense;
        public float CurrentEvade => currentEvade;

        public int BasePhysicalAttack => basePhysicalAttack;
        public int BaseMagicalAttack => baseMagicalAttack;
        public float BaseAccuracy => baseAccuracy;

        public int BasePhysicalDefense => basePhysicalDefense;
        public int BaseMagicalDefense => baseMagicalDefense;
        public float BaseEvade => baseEvade;

        public void InitializeNewPartyMemberFromScriptableObject(PartyMemberBaseData baseData)
        {
            currentLevel = baseData.baseLevel;
            
            basePhysicalAttack = baseData.basePhysicalAttack;
            baseMagicalAttack = baseData.baseMagicalAttack;
            baseAccuracy = baseData.baseAccuracy;

            basePhysicalDefense = baseData.basePhysicalDefense;
            baseMagicalDefense = baseData.baseMagicalDefense;
            baseEvade = baseData.baseEvade;
                
            ResetCurrentStatsToBase();
        }

        private void ResetCurrentStatsToBase()
        {
            currentPhysicalAttack = basePhysicalAttack;
            currentMagicalAttack = baseMagicalAttack;
            currentAccuracy = baseAccuracy;
            
            currentPhysicalDefense = basePhysicalDefense;
            currentMagicalDefense = baseMagicalDefense;
            currentEvade = baseEvade;
        }
    }
}

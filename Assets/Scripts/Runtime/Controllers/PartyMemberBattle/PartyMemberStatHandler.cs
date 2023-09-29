using Akashic.ScriptableObjects.Scripts;
using Akashic.ScriptableObjects.Scripts.PartyMemberBase;
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
        
        public int CurrentLevel { get => currentLevel; private set => currentLevel = value; }
        public int CurrentPhysicalAttack { get => currentPhysicalAttack; private set => currentPhysicalAttack = value; }
        public int CurrentMagicalAttack { get => currentMagicalAttack; private set => currentMagicalAttack = value; }
        public float CurrentAccuracy { get => currentAccuracy; private set => currentAccuracy = value; }
        
        public int CurrentPhysicalDefense { get => currentPhysicalDefense; private set => currentPhysicalDefense = value; }
        public int CurrentMagicalDefense { get => currentMagicalDefense; private set => currentMagicalDefense = value; }
        public float CurrentEvade { get => currentEvade; private set => currentEvade = value; }
        
        public int BasePhysicalAttack { get => basePhysicalAttack; }
        public int BaseMagicalAttack { get => baseMagicalAttack; }
        public float BaseAccuracy { get => baseAccuracy; }
        
        public int BasePhysicalDefense { get => basePhysicalDefense; }
        public int BaseMagicalDefense { get => baseMagicalDefense; }
        public float BaseEvade { get => baseEvade; }

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

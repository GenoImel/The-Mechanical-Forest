using UnityEngine;
using UnityEngine.Serialization;

namespace Akashic.Runtime.Character
{
    internal sealed class CharacterStats : MonoBehaviour
    {
        [SerializeField] private int baseLevel = 25;
        [SerializeField] private int baseExp;
        [SerializeField] private int baseHealth = 380;
        [SerializeField] private int basePhysicalAttack = 45;
        [SerializeField] private int baseMagicalAttack = 165;
        [SerializeField] private int baseAccuracy = 20;
        [SerializeField] private int baseEvade = 15;
        [SerializeField] private int basePhysicalDefense = 70;
        [SerializeField] private int baseMagicalDefense = 100;

        private int currentLevel;
        private int currentExp;
        private int currentHealth;
        private int currentPhysicalAttack;
        private int currentMagicalAttack;
        private int currentAccuracy;
        private int currentEvade;
        private int currentPhysicalDefense;
        private int currentMagicalDefense;

        public void Start()
        {

            currentLevel = baseLevel;
            currentExp = baseExp;
            currentHealth = baseHealth;
            currentPhysicalAttack = basePhysicalAttack;
            currentMagicalAttack = baseMagicalAttack;
            currentAccuracy = baseAccuracy;
            currentEvade = baseEvade;
            currentPhysicalDefense = basePhysicalDefense;
            currentMagicalDefense = baseMagicalDefense;
        }
    }
}

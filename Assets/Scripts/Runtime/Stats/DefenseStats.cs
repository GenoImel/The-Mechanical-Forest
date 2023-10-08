using Akashic.ScriptableObjects.PartyMemberBase;
using UnityEngine;

namespace Akashic.Runtime.Stats
{
    internal sealed class DefenseStats
    {
        [SerializeField] private int physicalDefense;
        [SerializeField] private int magicalDefense;
        [SerializeField] private float evade;

        public DefenseStats(int physicalDefense, int magicalDefense, float evade)
        {
            this.physicalDefense = physicalDefense;
            this.magicalDefense = magicalDefense;
            this.evade = evade;
        }

        public DefenseStats(PartyMemberBaseData baseData)
        {
            physicalDefense = baseData.basePhysicalDefense;
            magicalDefense = baseData.baseMagicalDefense;
            evade = baseData.baseEvade;
        }
    }
}
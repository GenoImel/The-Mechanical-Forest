using Akashic.ScriptableObjects.PartyMemberBase;
using System;
using UnityEngine;

namespace Akashic.Runtime.Stats
{
    [Serializable]
    internal sealed class DefenseStats
    {
        [SerializeField] private int physicalDefense;
        [SerializeField] private int magicalDefense;
        [SerializeField] private float evade;

        public int PhysicalDefense => physicalDefense;
        public int MagicalDefense => magicalDefense;
        public float Evade => evade;

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
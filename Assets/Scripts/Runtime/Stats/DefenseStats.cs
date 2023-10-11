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

        /// <summary>Initializes the defense stats</summary>
        /// <param name="physDefense">Physical Defense</param>
        /// <param name="magDefense">Magical Defense</param>
        /// <param name="ev">Evade</param>
        public DefenseStats(int physDefense, int magDefense, float ev)
        {
            physicalDefense = physDefense;
            magicalDefense = magDefense;
            evade = ev;
        }

        public DefenseStats(PartyMemberBaseData baseData)
        {
            physicalDefense = baseData.basePhysicalDefense;
            magicalDefense = baseData.baseMagicalDefense;
            evade = baseData.baseEvade;
        }
    }
}
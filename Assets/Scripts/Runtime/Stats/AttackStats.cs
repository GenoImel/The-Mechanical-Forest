using System;
using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.Stats
{
    [Serializable]
    internal sealed class AttackStats
    {
        [SerializeField] private int physicalAttack;
        [SerializeField] private int magicalAttack;
        [SerializeField] private float accuracy;

        public int PhysicalAttack => physicalAttack;
        public int MagicalAttack => magicalAttack;
        public float Accuracy => accuracy;

        /// <summary>Initializes the attack stats</summary>
        /// <param name="physAttack">Physical Attack</param>
        /// <param name="magAttack">Magical Attack</param>
        /// <param name="acc">Accuracy</param>
        public AttackStats(int physAttack, int magAttack, float acc)
        {
            physicalAttack = physAttack;
            magicalAttack = magAttack;
            accuracy = acc;
        }

        public AttackStats(PartyMemberData partyMemberData)
        {
            physicalAttack = partyMemberData.basePhysicalAttack;
            magicalAttack = partyMemberData.baseMagicalAttack;
            accuracy = partyMemberData.baseAccuracy;
        }
    }
}
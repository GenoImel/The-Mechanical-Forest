using System;
using Akashic.ScriptableObjects.PartyMemberBase;
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

        public AttackStats(int physicalAttack, int magicalAttack, float accuracy)
        {
            this.physicalAttack = physicalAttack;
            this.magicalAttack = magicalAttack;
            this.accuracy = accuracy;
        }

        public AttackStats(PartyMemberBaseData baseData)
        {
            physicalAttack = baseData.basePhysicalAttack;
            magicalAttack = baseData.baseMagicalAttack;
            accuracy = baseData.baseAccuracy;
        }
    }
}
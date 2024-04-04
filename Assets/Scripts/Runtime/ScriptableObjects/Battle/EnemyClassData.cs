using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Battle
{
    [CreateAssetMenu(menuName = "Akashic/Battle/New Enemy Class Data")]
    internal sealed class EnemyClassData : ScriptableObject
    {
        public string className;
        public string classId;
        public string classDescription;

        [Header("Max Stats")]
        public int maxMight;
        public int maxDeftness;
        public int maxTenacity;
        public int maxResolve;
        
        public DamageType damageType;
        public SpeedStat speedStat;
    }
}
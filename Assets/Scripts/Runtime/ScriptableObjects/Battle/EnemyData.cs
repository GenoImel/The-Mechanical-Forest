using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.Actors.Battle.Enemy;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Battle
{
    [CreateAssetMenu(menuName = "Akashic/Battle/New Enemy Data")]
    internal sealed class EnemyData : ScriptableObject
    {
        public string enemyName;
        public string enemyId;
        public string enemyDescription;
        
        public EnemyClassData enemyClass;

        [Header("Stats")]
        public int mightModifier;
        public int deftnessModifier;
        public int tenacityModifier;
        public int resolveModifier;

        [Header("Behaviour")]
        public IEnemyBehaviour behaviour;
        
        public EnemyBattleActor enemyBattleActor;
    }
}
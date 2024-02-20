using Akashic.Runtime.Actors.Battle;
using UnityEngine;

namespace Akashic.ScriptableObjects.Battle
{
    [CreateAssetMenu(menuName = "Akashic/Battle/New Enemy Data")]
    internal sealed class EnemyData : ScriptableObject
    {
        public string enemyName;
        public string enemyId;
        public string enemyDescription;
        public EnemyBattleActor enemyBattleActor;
    }
}
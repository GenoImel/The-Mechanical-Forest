using System.Collections.Generic;
using Akashic.Runtime.Actors.Battle;
using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.ScriptableObjects.Battle
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

        [Header("Skills")]
        public List<SkillData> skills;
        
        public EnemyBattleActor enemyBattleActor;
    }
}
using Akashic.Runtime.Actors.Battle.Base;
using Akashic.Runtime.StatusEffects;
using UnityEngine;

namespace Akashic.Runtime.ScriptableObjects.Battle
{
    [CreateAssetMenu(menuName = "Akashic/Skills/New Skill")]
    internal sealed class SkillData : ScriptableObject
    {
        [Header("Info")]
		public string skillName;
        public string skillId;
        public string description;
        [SerializeReference] public BaseSkill scripting;

        [Header("Stats")]
        [Range(1, 10)] public int apCost;
        [Range(0, 3)] public int pipCost;
        [Range(0, 999)] public int damageModifier;
        public bool canCrit;

        [Header("Status Info")]
        public Status statusEffect;
        [Range(0, 1)] public float statusEffectChance;
        [Range(0, 5)] public int statusEffectDuration;

        [Header("Graphics")]
        public Sprite buttonDescriptionArtwork;
        public Sprite timelineArtwork;

        public BaseSkill GetSkillScript()
        {
            scripting.SetSkillData(this);
            return scripting;
        }
    }
}

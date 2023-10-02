using Akashic.Runtime.Skills;
using Akashic.Runtime.StatusEffects;
using UnityEngine;

namespace Akashic.ScriptableObjects.PartyMemberBase
{
    [CreateAssetMenu(menuName = "Akashic/Skills/New Base Skill")]
    internal sealed class SkillBaseData : ScriptableObject
    {
        [Header("Info")] 
        public string skillName;
        public string description;
        public SkillElement skillElement;

        [Header("Stats")]
        [Range(1, 10)] public int apCost;
        [Range(0, 999)] public int baseDamage;

        [Header("Status Info")]
        public Status statusEffect;
        [Range(0, 1)] public float statusEffectChance;
        [Range(0, 5)] public int statusEffectDuration;

        [Header("Graphics")]
        public Sprite buttonDescriptionArtwork;
        public Sprite timelineArtwork;
    }
}

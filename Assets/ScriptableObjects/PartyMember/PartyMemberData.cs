using Akashic.ScriptableObjects.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Akashic.ScriptableObjects.PartyMember
{
    [CreateAssetMenu(menuName = "Akashic/Party Member/New Party Member")]
    internal sealed class PartyMemberData : ScriptableObject
    {
        [Header("Info")] 
        public string partyMemberName;

        [Header("Leveling")]
        [Range(0, 99)] public int baseLevel;
        [Range(0, 1)] public float baseExp;

        [Header("Stats")]
        public int maxMight;
        public int maxDeftness;
        public int maxTenacity;
        public int maxResolve;

		[Header("Skills")]
		public List<SkillData> skills;

		[Header("Equipment")]
		public WeaponData weapon;
		public ArmorData armor;
		public RelicData relic;
		public List<AccessoryData> accessories;
    }
}

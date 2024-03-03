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
        [Range(0, 1)] public float baseExp; // float for percent experience to next level

        [Header("Stats")]
        public int baseMight;
        public int baseDeftness;
        public int baseTenacity;
        public int baseResolve;

		[Header("Skills")]
		public List<SkillData> skills;

		[Header("Equipment")]
		public WeaponData weapon;
		public ArmorData armor;
		public RelicData relic;
		public List<AccessoryData> accessories;
    }
}

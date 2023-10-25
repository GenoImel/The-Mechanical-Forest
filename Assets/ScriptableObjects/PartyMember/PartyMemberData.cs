using UnityEngine;

namespace Akashic.ScriptableObjects.PartyMember
{
    [CreateAssetMenu(menuName = "Akashic/Party Member/New Party Member")]
    internal sealed class PartyMemberData : ScriptableObject
    {
        [Header("Info")] 
        public string partyMemberName;
    
        [Header("Health")]
        [Range(0, 999)] public int baseHealth;

        [Header("Leveling")]
        [Range(0, 99)] public int baseLevel;
        [Range(0, 99)] public int baseExp;

        [Header("Attack")]
        [Range(0, 99)] public int basePhysicalAttack;
        [Range(0, 99)] public int baseMagicalAttack;
        [Range(0, 10)] public float baseAccuracy;

        [Header("Defenses")]
        [Range(0, 99)] public int basePhysicalDefense;
        [Range(0, 99)] public int baseMagicalDefense;
        [Range(0, 10)] public float baseEvade;
    }
}

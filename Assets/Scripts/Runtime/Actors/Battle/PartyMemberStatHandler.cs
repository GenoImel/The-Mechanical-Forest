using Akashic.Runtime.Stats;
using Akashic.ScriptableObjects.PartyMember;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class PartyMemberStatHandler : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int currentLevel;

        [Header("Current Stats")]
        [SerializeField] private AttackStats currentAttackStats;
        [SerializeField] private DefenseStats currentDefenseStats;

        [Header("Base Stats")]
        [SerializeField] private AttackStats baseAttackStats;
        [SerializeField] private DefenseStats baseDefenseStats;
        
        public int CurrentLevel => currentLevel;

        public AttackStats CurrentAttackStats => currentAttackStats;
        public AttackStats BaseAttackStats => baseAttackStats;
        public DefenseStats CurrentDefenseStats => currentDefenseStats;
        public DefenseStats BaseDefenseStats => baseDefenseStats;

        public void InitializeNewPartyMemberFromScriptableObject(PartyMemberData partyMemberData)
        {
            currentLevel = partyMemberData.baseLevel;

            baseAttackStats = new AttackStats(partyMemberData);
            baseDefenseStats = new DefenseStats(partyMemberData);
                
            ResetCurrentStatsToBase();
        }

        private void ResetCurrentStatsToBase()
        {
            currentAttackStats = baseAttackStats;
            currentDefenseStats = baseDefenseStats;
        }
    }
}

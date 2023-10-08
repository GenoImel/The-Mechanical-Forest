using Akashic.Runtime.Stats;
using Akashic.ScriptableObjects.PartyMemberBase;
using UnityEngine;

namespace Akashic.Runtime.Controllers.PartyMemberBattle
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

        public void InitializeNewPartyMemberFromScriptableObject(PartyMemberBaseData baseData)
        {
            currentLevel = baseData.baseLevel;

            baseAttackStats = new AttackStats(baseData);
            baseDefenseStats = new DefenseStats(baseData);
                
            ResetCurrentStatsToBase();
        }

        private void ResetCurrentStatsToBase()
        {
            currentAttackStats = baseAttackStats;
            currentDefenseStats = baseDefenseStats;
        }
    }
}

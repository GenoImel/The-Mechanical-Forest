using Akashic.Runtime.Stats;
using Akashic.ScriptableObjects.PartyMemberBase;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Akashic.Runtime.Controllers.PartyMemberBattle
{
    internal sealed class PartyMemberStatHandler : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int currentLevel;

        [SerializeField] private AttackStats currentAttackStats;
        [SerializeField] private AttackStats baseAttackStats;

        [SerializeField] private DefenseStats currentDefenseStats;
        [SerializeField] private DefenseStats baseDefenseStats;
        
        public int CurrentLevel => currentLevel;

        [SerializeField] private AttackStats CurrentAttackStats => currentAttackStats;
        [SerializeField] private AttackStats BaseAttackStats => baseAttackStats;
        [SerializeField] private DefenseStats CurrentDefenseStats => currentDefenseStats;
        [SerializeField] private DefenseStats BaseDefenseStats => baseDefenseStats;

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

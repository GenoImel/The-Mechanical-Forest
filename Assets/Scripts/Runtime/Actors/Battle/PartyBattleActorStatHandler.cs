using System;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.Serializers.Save;
using Akashic.ScriptableObjects.Config;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class PartyBattleActorStatHandler : MonoBehaviour
    {
        [SerializeField] private int currentLevel;

        private HitPoints hitPoints;
        private Might might;
        private Deftness deftness;
        private Tenacity tenacity;
        private Resolve resolve;
        
        [Header("Resources")]
        [SerializeField] private int baseAbilityPoints;
        
        [SerializeField] private int bufferHitPoints;
        
        [SerializeField] private int actionPips;

        public int CurrentLevel => currentLevel;
        public int CurrentHitPoints => hitPoints.CurrentHitPoints;
        public int BaseAbilityPoints => baseAbilityPoints;
        public int CurrentMight => might.CalculatedMight;
        public int CurrentDeftness => deftness.CalculatedDeftness;
        public int CurrentTenacity => tenacity.CalculatedTenacity;
        public int CurrentResolve => resolve.CalculatedResolve;
        
        public int ActionPips => actionPips;
        public int BufferHitPoints => bufferHitPoints;

        private GameConfigData gameConfigData;
        
        private IConfigMonoSystem configMonoSystem;

        private void Awake()
        {
            configMonoSystem = FindObjectOfType<ConfigMonoSystem>();
            gameConfigData = configMonoSystem.GetBattleConfigData();
        }

        public void InitializePartyBattleActorStats(PartyMember partyMember)
        {
            currentLevel = partyMember.PartyMemberStats.Level;
            hitPoints = partyMember.PartyMemberStats.HitPoints;
            baseAbilityPoints = partyMember.PartyMemberStats.BaseAbilityPoints;
            might = partyMember.PartyMemberStats.Might;
            deftness = partyMember.PartyMemberStats.Deftness;
            tenacity = partyMember.PartyMemberStats.Tenacity;
            resolve = partyMember.PartyMemberStats.Resolve;
        }

        private void RefreshBufferHitPoints()
        {
            
        }
        
        private void RegeneratePips()
        {
            actionPips = gameConfigData.basePips;
        }
    }
}

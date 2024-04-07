using System;
using Akashic.Core;
using Akashic.Runtime.Actors.Battle.Environment;
using Akashic.Runtime.MonoSystems.Config;
using Akashic.Runtime.ScriptableObjects.Config;
using Akashic.Runtime.Serializers.Save;
using UnityEngine;

namespace Akashic.Runtime.Actors.Battle.Base
{
    internal abstract class BattleActorStatHandler : MonoBehaviour
    {
        [SerializeField] protected int currentLevel;
        
        protected HitPoints hitPoints;
        protected Might might;
        protected Deftness deftness;
        protected Tenacity tenacity;
        protected Resolve resolve;
        
        [Header("Resources")]
        [SerializeField] protected int baseAbilityPoints;
        
        [SerializeField] protected int bufferHitPoints;
        
        [SerializeField] protected int actionPips;
        
        public int CurrentLevel => currentLevel;
        
        public int CurrentHitPoints => hitPoints.CurrentHitPoints;
        public int MaxHitPoints => hitPoints.MaxHitPoints;
        public int BaseAbilityPoints => baseAbilityPoints;
        
        public int CurrentMight => might.CalculatedMight;
        public int CurrentDeftness => deftness.CalculatedDeftness;
        public int CurrentTenacity => tenacity.CalculatedTenacity;
        
        public int CurrentResolve => resolve.CalculatedResolve;
        public int ActionPips => actionPips;
        
        public int BufferHitPoints => bufferHitPoints;
        
        protected GameConfigData gameConfigData;
        
        protected IConfigMonoSystem configMonoSystem;
        
        protected virtual void Awake()
        {
            configMonoSystem = GameManager.GetMonoSystem<IConfigMonoSystem>();
            gameConfigData = configMonoSystem.GetBattleConfigData();
        }
        
        public abstract void InitializeBattleActorStats(BattleActorInitializationParameters parameters);

        protected abstract void RefreshBufferHitPoints();

        public void RemoveActionPips(int actionPipsToRemove)
        {
            if (actionPipsToRemove > actionPips)
            {
                throw new Exception($"You cannot remove more action pips than the {typeof(BattleActor)} has.");
            }
            
            actionPips -= actionPipsToRemove;
        }

        protected virtual void RegeneratePips()
        {
            actionPips = GameManager.GetMonoSystem<IConfigMonoSystem>().GetBattleConfigData().basePips;
        }
    }
}
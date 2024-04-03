using Akashic.Core;
using Akashic.Runtime.MonoSystems.Battle;
using Akashic.Runtime.MonoSystems.Timeline;
using Akashic.Runtime.StateMachines.BattleStates;
using Akashic.Runtime.StateMachines.TurnStates;
using UnityEngine;

namespace Akashic.Runtime.SubCoreModules.Battle
{
    internal sealed class BattleSubCoreModule : SubCoreModule
    {
        [Header("MonoSystems")]
        [SerializeField] private PartyBattleMonoSystem partyBattleMonoSystem;
        [SerializeField] private EnemyBattleMonoSystem enemyBattleMonoSystem;
        [SerializeField] private TimelineMonoSystem timelineMonoSystem;
        
        [Header("StateMachines")]
        [SerializeField] private BattleStateMachine battleStateMachine;
        [SerializeField] private TurnStateMachine turnStateMachine;

        public override void OnLoaded()
        {
            SetSubCoreParentsActive();
            Debug.Log($"{nameof(BattleSubCoreModule)} sub core module has been loaded.");
        }

        public override void OnUnloaded()
        {
            Debug.Log($"{nameof(BattleSubCoreModule)} sub core module has been unloaded.");
            SetSubCoreParentsInactive();
        }

        protected override void InitializeSubCoreStateMachines()
        {
            stateMachineManager.AddStateMachine<BattleStateMachine, IBattleStateMachine>(battleStateMachine);
            stateMachineManager.AddStateMachine<TurnStateMachine, ITurnStateMachine>(turnStateMachine);
        }

        protected override void InitializeSubCoreMonoSystems()
        {
            monoSystemManager.AddMonoSystem<PartyBattleMonoSystem, IPartyBattleMonoSystem>(partyBattleMonoSystem);
            monoSystemManager.AddMonoSystem<EnemyBattleMonoSystem, IEnemyBattleMonoSystem>(enemyBattleMonoSystem);
            monoSystemManager.AddMonoSystem<TimelineMonoSystem, ITimelineMonoSystem>(timelineMonoSystem);
        }
        
        protected override void SetSubCoreParentsActive()
        {
            stateMachinesParentTransform.gameObject.SetActive(true);
            monoSystemsParentTransform.gameObject.SetActive(true);
            actorsParentTransform.gameObject.SetActive(true);
            controllersParentTransform.gameObject.SetActive(true);
        }
        
        protected override void SetSubCoreParentsInactive()
        {
            stateMachinesParentTransform.gameObject.SetActive(false);
            monoSystemsParentTransform.gameObject.SetActive(false);
            actorsParentTransform.gameObject.SetActive(false);
            controllersParentTransform.gameObject.SetActive(false);
        }
    }
}
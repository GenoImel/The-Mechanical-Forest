using Akashic.Core;
using Akashic.Runtime.StateMachines.BattleStates;
using UnityEngine;

namespace Akashic.Runtime.SubCoreModules.Battle
{
    internal sealed class BattleSubCoreModule : SubCoreModule
    {
        [Header("Management")] 
        [SerializeField] private Transform stateMachinesParentTransform;
        
        [SerializeField] private Transform monoSystemsParentTransform;

        [SerializeField] private Transform actorsParentTransform;
        
        [SerializeField] private Transform controllersParentTransform;
        
        [Header("StateMachines")]
        [SerializeField] private BattleStateMachine battleStateMachine;

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
        }

        protected override void InitializeSubCoreMonoSystems()
        {
            
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
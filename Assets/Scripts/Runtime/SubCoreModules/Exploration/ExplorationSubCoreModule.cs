using Akashic.Core;
using Akashic.Runtime.StateMachines.ExplorationStates;
using UnityEngine;

namespace Akashic.Runtime.SubCoreModules.Exploration
{
    internal sealed class ExplorationSubCoreModule : SubCoreModule
    {
        [Header("StateMachines")]
        [SerializeField] private ExplorationStateMachine explorationStateMachine;
        
        public override void OnLoaded()
        {
            SetSubCoreParentsActive();
            Debug.Log($"{nameof(ExplorationSubCoreModule)} sub core module has been loaded.");
        }

        public override void OnUnloaded()
        {
            Debug.Log($"{nameof(ExplorationSubCoreModule)} sub core module has been unloaded.");
            SetSubCoreParentsInactive();
        }

        protected override void InitializeSubCoreStateMachines()
        {
            stateMachineManager.AddStateMachine
                <ExplorationStateMachine, IExplorationStateMachine>(explorationStateMachine);
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
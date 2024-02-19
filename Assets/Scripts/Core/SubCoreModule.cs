using Akashic.Core.MonoSystems;
using Akashic.Core.StateMachines;
using UnityEngine;

namespace Akashic.Core
{
    internal abstract class SubCoreModule : MonoBehaviour
    {
        protected readonly StateMachineManager stateMachineManager = new ();
        protected readonly MonoSystemManager monoSystemManager = new ();
        
        public StateMachineManager StateMachineManager => stateMachineManager;
        public MonoSystemManager MonoSystemManager => monoSystemManager;
        
        protected virtual void Awake()
        {
            InitializeSubCoreStateMachines();
            InitializeSubCoreMonoSystems();

            GameManager.LoadSubCoreModule(this);
        }
        
        protected virtual void OnDestroy()
        {
            GameManager.UnloadSubCoreModule(this);
        }

        public abstract void OnLoaded();

        public abstract void OnUnloaded();

        /// <summary>
        /// Called when bootstrapping sub core module state machines.
        /// </summary>
        protected abstract void InitializeSubCoreStateMachines();
        
        /// <summary>
        /// Called when bootstrapping sub core module services.
        /// </summary>
        protected abstract void InitializeSubCoreMonoSystems();

        /// <summary>
        /// Called after bootstrapping complete.
        /// Sets all parent transforms active.
        /// </summary>
        protected abstract void SetSubCoreParentsActive();
        
        /// <summary>
        /// Called after the sub core module is unloaded.
        /// </summary>
        protected abstract void SetSubCoreParentsInactive();
    }
}
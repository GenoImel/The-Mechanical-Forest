using System;
using Akashic.Core.Messages;
using Akashic.Core.MonoSystems;
using Akashic.Core.StateMachines;
using UnityEngine;

namespace Akashic.Core
{
    internal abstract class GameManager : MonoBehaviour
    {
        private const string GameManagerPrefabPath = "GameManager";
        private static GameManager instance;
        
        private readonly MessageManager messageManager = new ();
        private readonly StateMachineManager stateMachineManager = new ();
        private readonly MonoSystemManager monoSystemManager = new ();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (instance)
            {
                return;
            }

            var gameManagerPrefab = Resources.Load<GameManager>(GameManagerPrefabPath);
            var gameManager = Instantiate(gameManagerPrefab);

            gameManager.name = gameManager.GetApplicationName();
            
            DontDestroyOnLoad(gameManager);

            instance = gameManager;

            gameManager.OnInitialized();
        }

        /// <summary>
        /// Adds a listener for a message event.
        /// </summary>
        public static void AddListener<TMessage>(Action<TMessage> listener) where TMessage : IMessage
        {
            instance.messageManager.AddListener(listener);
        }

        /// <summary>
        /// Removes a listener for a message event.
        /// </summary>
        public static void RemoveListener<TMessage>(Action<TMessage> listener) where TMessage : IMessage
        {
            instance.messageManager.RemoveListener(listener);
        }

        /// <summary>
        /// Publishes a message event over the message bus.
        /// </summary>
        public static void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            instance.messageManager.Publish(message);
        }
        
        /// <returns>
        /// Returns an existing State Machine in the game.
        /// </returns>
        public static TStateMachine GetStateMachine<TStateMachine>()
        {
            return instance.stateMachineManager.GetStateMachine<TStateMachine>();
        }
        
        /// <summary>
        /// Adds a State Machine to the game.
        /// </summary>
        protected void AddStateMachine<TStateMachine, TBindTo>(TStateMachine stateMachine) 
            where TStateMachine : IStateMachine, TBindTo
        {
            stateMachineManager.AddStateMachine<TStateMachine, TBindTo>(stateMachine);
        }
        
        /// <summary>
        /// Removes a State Machine from the game.
        /// </summary>
        protected void RemoveStateMachine<TStateMachine, TBindTo>(TStateMachine stateMachine)
        where TStateMachine : IStateMachine, TBindTo
        {
            stateMachineManager.RemoveStateMachine<TStateMachine, TBindTo>(stateMachine);
        }

        /// <returns>
        /// Returns an existing MonoSystem in the game.
        /// </returns>
        public static TMonoSystem GetMonoSystem<TMonoSystem>()
        {
            return instance.monoSystemManager.GetMonoSystem<TMonoSystem>();
        }
        
        /// <summary>
        /// Adds a MonoSystem to the game.
        /// </summary>
        protected void AddMonoSystem<TMonoSystem, TBindTo>(TMonoSystem monoSystem) 
            where TMonoSystem : IMonoSystem, TBindTo
        {
            monoSystemManager.AddMonoSystem<TMonoSystem, TBindTo>(monoSystem);
        }
        
        /// <summary>
        /// Removes a MonoSystem to the game.
        /// </summary>
        protected void RemoveMonoSystem<TMonoSystem, TBindTo>(TMonoSystem monoSystem) 
            where TMonoSystem : IMonoSystem, TBindTo
        {
            monoSystemManager.RemoveMonoSystem<TMonoSystem, TBindTo>(monoSystem);
        }
        
        /// <summary>
        /// Loads a sub core module into the game.
        /// </summary>
        public static void LoadSubCoreModule(SubCoreModule subCoreModule)
        {
            foreach (var (bindToType, stateMachine) in subCoreModule.StateMachineManager.StateMachines)
            {
                instance.stateMachineManager.AddStateMachine(stateMachine, bindToType);
            }
            
            foreach (var (bindToType, monoSystem) in subCoreModule.MonoSystemManager.MonoSystems)
            {
                instance.monoSystemManager.AddMonoSystem(monoSystem, bindToType);
            }
            
            subCoreModule.OnLoaded();
        }
        
        
        /// <summary>
        /// Unloads a sub core module from the game.
        /// </summary>
        public static void UnloadSubCoreModule(SubCoreModule subCoreModule)
        {
            foreach (var (bindToType, stateMachine) in subCoreModule.StateMachineManager.StateMachines)
            {
                instance.stateMachineManager.RemoveStateMachine(stateMachine, bindToType);
            }
            
            foreach (var (bindToType, monoSystem) in subCoreModule.MonoSystemManager.MonoSystems)
            {
                instance.monoSystemManager.RemoveMonoSystem(monoSystem, bindToType);
            }
            
            subCoreModule.OnUnloaded();
        }

        /// <returns>
        /// Name of the application or game.
        /// </returns>
        protected abstract string GetApplicationName();

        /// <summary>
        /// Called when the game manager is initialized.
        /// </summary>
        protected abstract void OnInitialized();
        
        /// <summary>
        /// Called when bootstrapping game state machines.
        /// </summary>
        protected abstract void InitializeGameStateMachines();
        
        /// <summary>
        /// Called when bootstrapping game services.
        /// </summary>
        protected abstract void InitializeGameMonoSystems();

        /// <summary>
        /// Called after bootstrapping complete.
        /// Sets all parent transforms active.
        /// </summary>
        protected abstract void SetParentsActive();
    }
}
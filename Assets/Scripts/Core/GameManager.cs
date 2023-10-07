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
        /// Adds a new message listener.
        /// </summary>
        public static void AddListener<TMessage>(Action<TMessage> listener) where TMessage : IMessage
        {
            instance.messageManager.AddListener(listener);
        }

        /// <summary>
        /// Removes a message listener.
        /// </summary>
        public static void RemoveListener<TMessage>(Action<TMessage> listener) where TMessage : IMessage
        {
            instance.messageManager.RemoveListener(listener);
        }

        /// <summary>
        /// Publishes a message to the monoSystems in the game.
        /// </summary>
        public static void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            instance.messageManager.Publish(message);
        }
        
        /// <returns>
        /// Returns an existing state in the game.
        /// </returns>
        public static TStateMachine GetStateMachine<TStateMachine>()
        {
            return instance.stateMachineManager.GetStateMachine<TStateMachine>();
        }
        
        /// <summary>
        /// Adds a State to the game.
        /// </summary>
        protected void AddStateMachine<TStateMachine, TBindTo>(TStateMachine stateMachine) 
            where TStateMachine : IStateMachine, TBindTo
        {
            stateMachineManager.AddStateMachine<TStateMachine, TBindTo>(stateMachine);
        }

        /// <returns>
        /// Returns an existing monoSystem in the game.
        /// </returns>
        public static TMonoSystem GetMonoSystem<TMonoSystem>()
        {
            return instance.monoSystemManager.GetMonoSystem<TMonoSystem>();
        }
        
        /// <summary>
        /// Adds a monoSystem to the game.
        /// </summary>
        protected void AddMonoSystem<TMonoSystem, TBindTo>(TMonoSystem monoSystem) where TMonoSystem : IMonoSystem, TBindTo
        {
            monoSystemManager.AddMonoSystem<TMonoSystem, TBindTo>(monoSystem);
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
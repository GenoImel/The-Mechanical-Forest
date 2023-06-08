# Core Architecture

The core archicture for the game follows the philosophy of limiting the number of singletons in the codebase, and instead using a single `GameManager` singleton to coordinate communication between decoupled `MonoBehaviour` scripts using `Message Events`, as well as enabling code and data sharing between `MonoBehaviours` by providing access to `MonoSystems`. 

This core architecture was originally created by a colleague that I studied under. I have made a few small modifications and am continuing to find ways to iterate on the architecture. If changes or modifications are needed during the course of this particular project, this document will be updated.

## Game Manager

Starting from the top of the architecture, let's take a look at the `GameManager` class:

```csharp
using System;
using UnityEngine;

// This script is intended to be the only singleton in the application.
// If a need arises for another singleton, we should consider reworking
// the architecture to fit the new use-case.
//
// The GameManager gives access to MonoSystems, which enable cross-platform
// code and data sharing. It also provides a Message Bus for creating and
// publishing Message Events, which are a decoupled alternative to Unity/C#
// actions, events, and delegates.
namespace Akashic.Core
{
    // The class definition for our GameManager is:
    // - internal to restrict access to Core architecture to the namespace.
    // - abstract, as this class is inherited by the AkashicGameManager script.
    // - Inherits from MonoBehaviour, so the core architecture works within
    //   Unity's MonoBehaviour framework.
   internal abstract class GameManager : MonoBehaviour
    {
        // We need to reference the GameManager prefab that exists in "Assets/Resources".
        private const string GameManagerPrefabPath = "GameManager";
        private static GameManager instance; // The GameManager is our only singleton.
        
        private readonly MessageManager messageManager = new (); // Used for managing Message Events
        private readonly MonoSystemManager MonoSystemManager = new (); // Used for managing MonoSystems.

        // At runtime, prior to any scene being loaded, we instantiate the AkashicGameManager prefab.
        // This is convenient because it means that we can worry less about scenes containing the
        // correct scripts in order to run. All core dependencies will simply be loaded into any 
        // active scene when hitting the play button in the editor, or running the game in a standalone player.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            // If an instance of GameManager already exists, return.
            // No need to initialize a new one.
            if (instance)
            {
                return;
            }

            // Load the GameManager prefab from resources and instantiate it into the scene.
            var gameManagerPrefab = Resources.Load<GameManager>(GameManagerPrefabPath);
            var gameManager = Instantiate(gameManagerPrefab);

            // Give the prefab a name. This name will match the name of the script component.
            // For this codebase, that will be AkashicGameManager.
            gameManager.name = gameManager.GetApplicationName();
            
            // Our GameManager should be persistent between scenes.
            DontDestroyOnLoad(gameManager);

            // Assign the instance to the GameManager we just instantiated.
            instance = gameManager;

            // Call the protected method OnInitialized. This is empty in this abstract script,
            // but has some logic in AkashicGameManager, which inherits from GameManager.
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
        /// Publishes a message to the MonoSystems in the game.
        /// </summary>
        public static void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            instance.messageManager.Publish(message);
        }

        /// <returns>
        /// Returns an existing MonoSystem in the game.
        /// </returns>
        public static TMonoSystem GetMonoSystem<TMonoSystem>()
        {
            return instance.MonoSystemManager.GetMonoSystem<TMonoSystem>();
        }
        
        /// <summary>
        /// Adds a MonoSystem to the game.
        /// </summary>
        protected void AddMonoSystem<TMonoSystem, TBindTo>(TMonoSystem MonoSystem) where TMonoSystem : IMonoSystem, TBindTo
        {
            MonoSystemManager.AddMonoSystem<TMonoSystem, TBindTo>(MonoSystem);
        }
        
        /// <returns>
        /// Name of the application or game.
        /// </returns>
        protected abstract string GetApplicationName();

        /// <summary>
        /// Called when the game manager is initialized.
        /// </summary>
        protected abstract void OnInitialized();
    }
}
```
## Messages

The following sections describe how `Message Events` work within the core architecture, starting from the interface `IMessage` for generic typing, the creation of a `MessageListener`, and ends at describing the logic behind the `MessageManager`:

### IMessage

`IMessage` is used for generic typing of all Message Events in the game's codebase:

```csharp
namespace Akashic.Core
{
    // The IMessage interface is used to generically type all
    // Message Events so that regardless of the overlying type
    // of the Message Event, they can be added to the Message Listener
    // since they all inherit from the generic type of IMessage.
    public interface IMessage
    {
    }
}
```

### Message Listener

The `MessageListener` class allows other scripts to add, remove listeners for `Message Events` through the `GameManager`, and keeps track of `MessageEvents` of the same type in an `IList`. It also provides a method for publishing `Message Events` over the `Message Bus`:

```csharp
using System;
using System.Collections.Generic;
using UnityEngine;

// The MessageListener script provides an IList of available Message Events that 
// other scripts can listen for by using GameManager.AddListener().
// The MessageListener also allows scripts to publish Message Events over the
// Message Bus by using GameManager.Publish().
namespace Akashic.Core
{
    internal sealed class MessageListener<TMessage> : MessageListener where TMessage : IMessage
    {
        // This IList of MessageListeners is used to track all TMessage : IMessage.
        // listeners are added to the list during AddListener() and
        // removed from the list during RemoveListener().
        //
        // Multiple scripts may be listening for the same type of IMessage.
        // So for each type of IMessage, an listener exists which will be
        // stored in an IDictionary<Type, MessageListener> in our MessageManager.
        private readonly IList<Action<TMessage>> listeners = new List<Action<TMessage>>();

        // Used for housekeeping purposes. We'll need to iterate through the list at some points.
        public int ListenerCount => listeners.Count;

        // Add a listener for an TMessage : IMessage to our IList listeners.
        public void AddListener(Action<TMessage> listener)
        {
            listeners.Add(listener);
        }

        // Remove a listener for an IMessage : IMessage to our IList listeners.
        public void RemoveListener(Action<TMessage> listener)
        {
            listeners.Remove(listener);
        }

        // Publish a TMessage : IMessage over the Message Bus.
        public void Publish(TMessage message)
        {
            // Iterate over the list to make sure the Message Event exists.
            for (var index = listeners.Count - 1; index >= 0; index--)
            {
                var listener = listeners[index];
                try
                {
                    // If the TMessage : IMessage is found in our IList listeners,
                    // invoke the Message Event.
                    listener.Invoke(message);
                }
                catch(Exception exception)
                {
                    // If the Message Event is not found in our IList listeners
                    // Log an exception.
                    Debug.LogException(exception);
                }
            }
        }
    }
    
    // Abstract class to assist with generic typing via TMessage : IMessage
    internal abstract class MessageListener
    {
    }
}
```

### Message Manager

The `MessageManager` manages all of our listeners for each type of `IMessage` that scripts are listening for. It provides further logic for `AddListener()`, `RemoveListener()`, and `Publish()`:

```csharp
using System;
using System.Collections.Generic;

// The MessageManager script keeps an IDictionary<Type, MessageListener>
// in order to keep track of all MessageListeners of type IMessage
// that have been added, and remove MessageListeners. It also handles
// further logic for publishing Message Events via MessageListener.
namespace Akashic.Core
{
    // This class is internal to the Core namespace, and sealed to prevent 
    // extension through inheritance.
    internal sealed class MessageManager
    {
        // Create an IDictionary to pair our MessageListener of type IMessage
        // with their overlying Type as a key.
        private readonly IDictionary<Type, MessageListener> listeners =
            new Dictionary<Type, MessageListener>();

        // Adding a new MessageListener to the Message Bus.
        public void AddListener<TMessage>(Action<TMessage> listener) where TMessage : IMessage
        {
            // First we get the overlying type of the TMessage : IMessage.
            var listenerType = typeof(TMessage);
            
            // We then determine whether this MessageListener already exists in our
            // IDictionary of MessageListeners.
            if(listeners.TryGetValue(listenerType, out var existingMessageListener))
            {
                // If the Message Event we are adding already has an existing
                // Message Listener, we access the MessageListener associated 
                // with the listener's type, and then add the new Message Event
                // to that particular MessageListener.
                var typedMessageListener = (MessageListener<TMessage>)existingMessageListener;
                typedMessageListener.AddListener(listener);
            }
            else
            {
                // If the Message Event we are adding does not already have
                // An existing Message Listener, we create a new MessageListener
                // and then add the listener to the new MessageListener.
                var newMessageListener = new MessageListener<TMessage>();
                newMessageListener.AddListener(listener);

                // We also make sure to add this new <Type, MessageListener> pair to our
                // IDictionary listeners.
                listeners[listenerType] = newMessageListener;
            }
        }

        // Removing a MessageListener from the Message Bus.
        public void RemoveListener<TMessage>(Action<TMessage> listener) where TMessage : IMessage
        {
            // First we get the overlying type of the TMessage : IMessage.
            var listenerType = typeof(TMessage);
            
            // We then determine whether this MessageListener already exists in our
            // IDictionary of MessageListeners.
            if (listeners.TryGetValue(listenerType, out var existingMessageListener) == false)
            {
                // If it doesn't exist, we simply return. Nothing can be removed.
                return;
            }

            // If the MessageListener does exist, we access the MessageListener associated 
            // with the listener's type, and then remove the Message Event
            // from that particular MessageListener.
            var typedMessageListener = (MessageListener<TMessage>)existingMessageListener;
            typedMessageListener.RemoveListener(listener);

            // If all Message Events have been removed from the MessageListener
            // for this particular type, then remove the listener from our 
            // IDictionary of listeners.
            if (typedMessageListener.ListenerCount <= 0)
            {
                listeners.Remove(listenerType);
            }
        }

        // Publishing a Message Event over the Message Bus.
        public void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            // First we get the overlying type of the TMessage : IMessage.
            var listenerType = typeof(TMessage);
            
            // We then determine whether this MessageListener exists in our
            // IDictionary of MessageListeners.
            if (listeners.TryGetValue(listenerType, out var existingMessageListener) == false)
            {
                // If it doesn't exist, we simply return. Nothing can be published.
                return;
            }

            // If the MessageListener does exist, we access the MessageListener associated 
            // with the listener's type, and then publish the TMessage message
            // through the MessageListener associated with the Message Event.
            var typedMessageListener = (MessageListener<TMessage>)existingMessageListener;
            typedMessageListener.Publish(message);
        }
    }
}
```

## MonoSystems

`MonoSystems` are a key component within the core architecture. They take the place of multipe singletons, allowing for data and code sharing across the game's codebase without having to manage and maintain multiple different singletons. Since the `GameManager` automatically self-instantiates into any scene, there is no need to remember to add multiple different prefab singleton dependencies to create a new scene and ensure it runs correctly. The dependencies for these MonoSystems are directly injected into a class by leveraging the core architecture through `GameManager.GetMonoSystem<TMonoSystem>()`. 

Lets dig into the details on how `MonoSystems` work within the core architecture:

### IMonoSystem

The `IMonoSystem` interface allows for generic typing of `MonoSystems`:

```csharp
namespace Akashic.Core
{
    // The IMonoSystem interface is used to generically type all
    // MonoSystems so that regardless of the overlying type
    // of the MonoSystem, they can be bootstrapped into the
    // GameManager at runtime.
    public interface IMonoSystem
    {
    }
}
```

### MonoSystem Manager

The `MonoSystemManager` provides methods for bootstrapping `MonoSystems` into the game, and providing dependency injection of `MonoSystems` into scripts by calling `GameManager.GetMonoSystem<TMonoSystem>()`:

```csharp
using System;
using System.Collections.Generic;

// The MonoSystem Manager allows us to bootstrap MonoSystems generically typed
// via IMonoSystem by using the AddMonoSystem method. It also allows us to
// inject dependencies on MonoSystems in other scripts by using GetMonoSystem.
namespace Akashic.Core
{
    // This class is internal to the Core namespace, and sealed to prevent 
    // extension through inheritance.
    internal sealed class MonoSystemManager
    {
        // Create an IDictionary to pair each IMonoSystem with its type.
        private readonly IDictionary<Type, IMonoSystem> MonoSystems =
            new Dictionary<Type, IMonoSystem>();

        // Bootstrap a MonoSystem into the game at runtime. This is called in
        // OnInitialized in the AkashicGameManager.
        //
        // When bootstrapping a MonoSystem, we grab the MonoSystem's companion
        // MonoBehaviour class as a component from a GameObject.
        // The GameObject that the MonoSystem MonoBehaviour class resides on
        // should be parented to the MonoSystems child GameObject of our
        // GameManager prefab (found in resources).
        //
        // We then bind the TMonoSystem : IMonoSystem to its companion
        // MonoBehaviour class.
        public void AddMonoSystem<TMonoSystem, TBindTo>(TMonoSystem MonoSystem)
            where TMonoSystem : TBindTo, IMonoSystem
        {
            // The name of the GameObject that the MonoSystem MonoBehavior component
            // lives on cannot be null. Throw an exception if it is.
            //
            // If this happens, you probably forgot to add your child MonoSystem
            // GameObject to its serialized field in the GameManager prefab.
            if (MonoSystem == null)
            {
                throw new Exception($"{nameof(MonoSystem)} cannot be null");
            }

            // If the GameObject for the MonoSystem isn't null:
            // - Grab the overlying type from the MonoSystem's interface.
            // - Add <Type, IMonoSystem> pair to the IDictionary of MonoSystems.
            var MonoSystemType = typeof(TBindTo);
            MonoSystems[MonoSystemType] = MonoSystem;
        }

        // When getting a MonoSystem from the GameManager singleton, all
        // that is required is specifying the type of MonoSystem you are
        // trying to get.
        public TMonoSystem GetMonoSystem<TMonoSystem>()
        {
            // First we get the overlying type of the requested MonoSystem.
            var MonoSystemType = typeof(TMonoSystem);
            
            // We then check the IDictionary of MonoSystems for this type of MonoSystem.
            if (MonoSystems.TryGetValue(MonoSystemType, out var MonoSystem))
            {
                // If this type of MonoSystem exists in the IDictionary, we return
                // it as a TMonoSystem : IMonoSystem.
                return (TMonoSystem)MonoSystem;
            }

            // If the type of MonoSystem was not found in our IDictionary of MonoSystems,
            // we throw an exception.
            //
            // Typically when I run into this issue, it is because I forgot to
            // drag and drop the child GameObject associated with the MonoSystem into
            // the serialized field on the GameManager prefab.
            throw new Exception($"MonoSystem {MonoSystemType} does not exist");
        }
    }
}
```

## Akashic Game Manager

With all of these scripts together, we have formed the basis for our `AkashicGameManager` script. This script is a component of the root `GameObject` in the `GameManager` prefab in "Assets/Resources", and is where the `GameManager` script's `protected override void OnInitialized()` is called, which is where all `MonoSystems` are bootstrapped into the game at runtime. Let's take a closer look at it:

```csharp
using Akashic.Core;
using UnityEngine;

// The AkashicGameManager script is a component of the root GameObject
// on the GameManager prefab in Assets/Resources.
//
// This script contains references to our MonoSystems parent Transform,
// and the controllers parent Transform. These parent Transforms are 
// used for controlling the timing of when their Unity Lifecycle methods
// are executed, and ensures that no one will attempt to get MonoSystems
// or broadcast messages before the game has been bootstrapped.
namespace Akashic.Runtime
{
    // This class is internal to the Core namespace and sealed to prevent
    // extension via inheritance.
    //
    // We inherit from GameManager, which takes care of the logic for:
    // - Instantiating this singleton prior to the scene loading.
    // - Provides inherited methods for adding/removing/publishing Messages.
    // - Provides an inherited method for Adding MonoSystems.
    // - Provides an inherited method for getting MonoSystems for dependency
    //   injection enabling cross-platform data and code sharing.
    // - Allows us to override OnInitialized, an empty method called
    //   in the underlying GameManager script after the singleton is
    //   instantiated, where we can perform post-instantiation 
    //   operations for bootstrapping the game, or anything else
    //   that may be needed.
    internal sealed class AkashicGameManager : GameManager
    {
        // For every MonoSystem created for the game:
        // - Create a child GameObject under the MonoSystemsParentTransform
        // - Serialize a reference to the GameObject's MonoBehaviour MonoSystem
        //   component into the editor.
        [Header("MonoSystems")] 
        [SerializeField] 
        private ExampleMonoSystem exampleMonoSystem;
        
        // We use the MonoSystemsParentTransform for timing.
        // This ensures that all MonoSystems Awake() at the same time.
        [SerializeField] 
        private Transform MonoSystemsParentTransform;

        // We use the controllersParentTransform for timing.
        // This prevents any controllers on the GameManager prefab
        // from getting a MonoSystem before bootstrapping is complete.
        [Header("Management")] 
        [SerializeField] 
        private Transform controllersParentTransform;

        // We set the name of the instantiated GameManager prefab
        // to be the same as this script component. In our case,
        // it will be name AkashicGameManager.
        protected override string GetApplicationName()
        {
            return nameof(AkashicGameManager);
        }

        // OnInitialized() is called after the singleton GameManager
        // prefab has been instantiated into the scene. This is where
        // the bootstrapping happens.
        protected override void OnInitialized()
        {
            // For every MonoSystem we serialize into the GameManager,
            // we want to add a line where we use AddMonoSystem to add
            // the MonoSystem into the core architecture, and make it 
            // available through GetMonoSystem<TMonoSystem>().
            AddMonoSystem<ExampleMonoSystem, IExampleSystem>(exampleMonoSystem);

            // Once bootstrapping of MonoSystems is complete, we set the
            // MonoSystemsParentTransform to active, allowing the MonoSystems
            // on the child GameObjects to call Awake().
            MonoSystemsParentTransform.gameObject.SetActive(true);
            
            // We do the same with the controllersParentTransform
            // afterwards, so that their Awake() occurs after the
            // MonoSystems have been set active.
            controllersParentTransform.gameObject.SetActive(true);
        }
    }
}
```

## Game Manager Prefab Notes

It should be noted that one decision I have made regarding the prefab associated with the core architecture is that the `GameManager` prefab contains a child `GameObject` `EventSystem`, which listens for input using the new input system. This is an alternative to requiring every scene to have its own `EventSystem`. Avoid adding any `EventSystems` into any Unity scene throughout the codebase, as this can cause issues and the console in the editor will issue a warning.

![image](https://github.com/mrjeremy3341/ProjectNecro/assets/99087864/e9a315a4-1bf8-4688-ad86-2fb1d1dfec98)


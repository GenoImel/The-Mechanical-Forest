# Core Architecture

The core architecture for the game follows the philosophy of limiting the number of singletons in the codebase, and instead using a single `GameManager` singleton to act as a central hub to orchestrate cross game communication while maintaining flexibility, scalability, maintainability- all wrapped in a simplistic API that is intuitive for developers of all skills levels to learn and apply. 

The game uses a hybridized core architecture with a foundational philosophy that adheres to the following concepts:

- **Embracing Unity's Native Framework:** Prioritizing a `MonoBehaviour`-centric design that respects Unity's `GameObject`-`Component` pattern.
- **Modular Interfacing:** Implementing specialized `interfaces` like `MonoSystems` to foster seamless data and code interchangeability among decoupled game features.
- **Hierarchical State Management:** Using `interfaces` and classes tailored for Hierarchical State Machines (HSMs), underscored by `class`-driven `state` definitions.
- **Efficient Dependency Management:** Incorporating the service locator pattern to ensure swift and streamlined dependency injection across the board.
- **Robust Communication Framework:** Ensuring unhindered cross-game communication through `Message Events` relayed via a dedicated `Message Bus`.

While developers are empowered to quickly get started creating `MonoBehaviour` features and core game components by referencing our `CodingStandards.md` documentation, those who are interested in understanding the hybridized core architecture on a deeper level are encouraged to read through the commented version of core scripts below. 

## Game Manager

The GameManager is the central hub through which various game components and systems communicate. Its core tenets are in its "single-`singleton`" design – as it is intended to be the only `singleton` in the game. This design decision emerges from a philosophy of simplifying access points and reducing the complexity of the game's API.

### Distinct characteristics of the `GameManager`:
- **Singleton By Design:**

    - The uniqueness of the `GameManager` `singleton` ensures that key core components, such as `MonoSystems`, can be easily accessed and used across different features. This also reduces potential issues stemming from multiple instance management.

- **Dynamic Instantiation:**

    - The `GameManager` is dynamically instantiated prior to any scene load. This architecture choice ensures that all core dependencies are automatically incorporated into any active scene. As a result, developers can concentrate on the scenes' specific logic without constantly ensuring that foundational dependencies are integrated. While many enterprise-level games typically use only one or two scenes, having an automatically instantiating `GameManager` opens us up to the use of test scenes for testing new and experimental features outside of our primary game scene.

- **Message Management:**

    - Integrating a `MessageManager`, the `GameManager` offers a decoupled communication methodology. Instead of conventional Unity/C# `actions`, `events`, and `delegates`, it employs `Message Events` to enable flexible communication. The `GameManager` also provides `interfaces` for adding or removing `message listener`s and publishing `Message Events`. These methods streamline and standardize how various game components interact with each other.


- **MonoSystem Management and Interaction:**

    - The `MonoSystemManager` manages `MonoSystems` within the game, which facilitates data and code sharing. It also offers methods for retrieving and adding `MonoSystems` and `States`, and encapsulates the management of these core architectural components, ensuring that their usage are consistent.

- **Extensibility:**

    - Being abstract, the GameManager class has been structured to be extensible, and this extensibility is primarily exposed through the `AkashicGameManager`, where various methods are `overriden` in order to tailor the `GameManager`'s behaviour.

Let's start by stepping through the base `GameManager` class to gather a deeper understanding of how we are accomplishing the above concepts:

```csharp
using System;
using Akashic.Core.Messages;
using Akashic.Core.MonoSystems;
using Akashic.Core.StateMachines;
using UnityEngine;

// This script is intended to be the only singleton in the game.
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
        private readonly StateMachineManager stateMachineManager = new(); // Used for managing StateMachines.
        private readonly MonoSystemManager monoSystemManager = new (); // Used for managing MonoSystems.

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
            gameManager.name = gameManager.GetGameName();
            
            // Our GameManager should be persistent between scenes.
            DontDestroyOnLoad(gameManager);

            // Assign the instance to the GameManager we just instantiated.
            instance = gameManager;

            // Call the protected method OnInitialized(). This is empty in this abstract script,
            // but has some logic in AkashicGameManager, which inherits from GameManager.
            gameManager.OnInitialized();
        }

        /// <summary>
        /// Adds a new Message Listener.
        /// </summary>
        public static void AddListener<TMessage>(Action<TMessage> listener) where TMessage : IMessage
        {
            instance.messageManager.AddListener(listener);
        }

        /// <summary>
        /// Removes a Message Listener.
        /// </summary>
        public static void RemoveListener<TMessage>(Action<TMessage> listener) where TMessage : IMessage
        {
            instance.messageManager.RemoveListener(listener);
        }

        /// <summary>
        /// Publishes a Message Event across the Message Bus.
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
        
        /// <returns>
        /// Name of the game or game.
        /// </returns>
        protected abstract string GetGameName();

        /// <summary>
        /// Called when the game manager is initialized.
        /// </summary>
        protected abstract void OnInitialized();

        /// <summary>
        /// Called when bootstrapping game state machines.
        /// </summary>
        protected abstract void InitializeGameStateMachines();
        
        /// <summary>
        /// Called when bootstrapping game MonoSystems.
        /// </summary>
        protected abstract void InitializeGameMonoSystems();

        /// <summary>
        /// Called after bootstrapping complete.
        /// Sets all parent transforms active.
        /// </summary>
        protected abstract void SetParentsActive();
    }
}
```

The `GameManager` class serves as a foundational element in the game's architecture. Through its design, it centralizes management of key components such as `Message Events`, `StateMachines`, and `MonoSystems`. With a systematic initialization process, it ensures that essential dependencies are loaded in every scene. By offering methods for listener management, state machine, and system handling, as well as abstracted methods for initialization and setting up systems, it promotes a modular and organized approach. In essence, this class provides a simplified API for managing and orchestrating core functionalities within the game.

## Message Events

In the game's architecture, `Message Events` play a pivotal role in facilitating communication between different decoupled features, `StatMachines`, and `MonoSystems`. This section delves into the structural underpinnings of these `Message Events`, covering everything from the foundational `IMessage` interface, the implementation of a `MessageListener`, and to the management of `Message Events` by the `MessageManager`. Each of these elements collectively ensures decoupled and efficient messaging across the game.

### IMessage

At the core of the messaging system within the game lies the `IMessage` `interface`. Serving as a foundational touch point for all `Message Events`, `IMessage` offers a level of abstraction that ensures consistent typing across the entire codebase. Generic typing makes sure that any `Message Event`, regardless of its unique characteristics, can be incorporated seamlessly into the `MessageListener`. 

```csharp
namespace Akashic.Core.Messages
{
    // The IMessage interface is used to generically type all
    // Message Events so that regardless of the overlying type
    // of the Message Event, they can be added to the Message Listener
    // since they all inherit from the generic type of IMessage.
    //
    // When creating a new Message Event, we will inherit from
    // this IMessage interface.
    internal interface IMessage
    {
    }
}
```

### Message Listener

The `MessageListener` serves a crucial role within our messaging system. Acting as an intermediary, it tracks and manages `Message Events` using an `IList`. This class allows scripts to register or publish specific `Message Events` via the `Message Bus`. In the details below, we'll break down its structure and the methods.

```csharp
using System;
using System.Collections.Generic;
using UnityEngine;

// The MessageListener script provides an IList of available Message Events that 
// other scripts can listen for by using GameManager.AddListener().
// The MessageListener also allows scripts to publish Message Events over the
// Message Bus by using GameManager.Publish().
namespace Akashic.Core.Messages
{
    internal sealed class MessageListener<TMessage> : MessageListener where TMessage : IMessage
    {
        // This IList of MessageListeners is used to track all TMessage : IMessage.
        // listeners are added to the list during AddListener() and
        // removed from the list during RemoveListener().
        //
        // Multiple scripts may be listening for the same type of IMessage.
        // So for each type of IMessage, a listener exists which is
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
                    // If the Message Event is found in our IList listeners,
                    // invoke the Message Event.
                    listener.Invoke(message);
                }
                catch(Exception exception)
                {
                    // If the Message Event is not found in our IList listeners
                    // log an exception.
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

The `MessageManager` is the orchestrator of our `Message Bus`, centralizing the management of all `listeners` for each `IMessage` type. By using a structured `IDictionary`, it keeps track of `MessageListeners` and efficiently directs `Message Event` communication. Let's breakdown the `MessageListener` script to grasp how this manager optimizes the processes of adding, removing, and publishing messages in our architecture.

```csharp
using System;
using System.Collections.Generic;

// The MessageManager script keeps an IDictionary<Type, MessageListener>
// in order to keep track of all MessageListeners of type IMessage
// that have been added, and remove MessageListeners. It also handles
// further logic for publishing Message Events via MessageListener.
namespace Akashic.Core.Messages
{
    // This class is internal to the Core.Messages namespace, and sealed to prevent 
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
                // an existing Message Listener, we create a new MessageListener
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

`MonoSystems` streamline the core architecture by replacing the need for multiple `singletons`, ensuring efficient data and code sharing across the game. Instead of managing various prefab `singleton` dependencies, the `GameManager` centralizes these `MonoSystems`, simplifying our game's overall API. `GameManager.GetMonoSystem<TMonoSystem>()` makes it easy to integrate `MonoSystems` via direct dependency injection. This section delves into the technical details and implementation of `MonoSystems` within our architectural framework.

### IMonoSystem

The `IMonoSystem` `interface` serves a crucial role by enabling generic typing for all `MonoSystems` within the core architecture. This approach allows MonoSystems to be be intuitively accessed through the service locator pattern in the `GameManager`. This section delves into the specifics of the `IMonoSystem` `interface` and its role in facilitating efficient and type-safe `MonoSystem` retrieval.

```csharp
namespace Akashic.Core.MonoSystems
{
    // The IMonoSystem interface is used to generically type all
    // MonoSystems so that regardless of the overlying type
    // of the MonoSystem, they can be bootstrapped into the
    // GameManager at runtime.
    
    /// <summary>
    /// For generic typing of specific MonoSystems.
    /// </summary>
    internal interface IMonoSystem
    {
    }
}
```

### MonoSystem Manager

The `MonoSystemManager` is an integral component responsible for managing and providing access to the various `MonoSystems` within the game. Through its robust methods, it facilitates the bootstrapping of `MonoSystems` and ensures that dependencies are injected wherever they are needed. By using the service locator pattern, `MonoSystemManager` works within the `GameManager` to provide scripts with the requested `MonoSystems` through `GameManager.GetMonoSystem<TMonoSystem>()`. This section elaborates on the mechanics of the `MonoSystemManager` and how it optimizes `MonoSystem` management and retrieval.

```csharp
using System;
using System.Collections.Generic;

// The MonoSystem Manager allows us to bootstrap MonoSystems generically typed
// via IMonoSystem by using the AddMonoSystem method. It also allows us to
// inject dependencies on MonoSystems in other scripts by using GetMonoSystem.
namespace Akashic.Core.MonoSystems
{
    // This class is internal to the Core.MonoSystems namespace, and sealed to prevent 
    // extension through inheritance.
    internal sealed class MonoSystemManager
    {
        // Create an IDictionary to pair each IMonoSystem with its type.
        private readonly IDictionary<Type, IMonoSystem> monoSystems =
            new Dictionary<Type, IMonoSystem>();

        // Bootstrap a MonoSystem into the game at runtime. This is called in
        // OnInitialized() in the AkashicGameManager.
        //
        // When bootstrapping a MonoSystem, we grab the MonoSystem's companion
        // MonoBehaviour class as a component from a GameObject.
        // The GameObject that the MonoSystem MonoBehaviour class resides on
        // should be parented to the MonoSystems child GameObject of our
        // GameManager prefab (found in resources).
        //
        // We then bind the TMonoSystem : IMonoSystem to its companion
        // MonoBehaviour class.
        public void AddMonoSystem<TMonoSystem, TBindTo>(TMonoSystem monoSystem)
            where TMonoSystem : TBindTo, IMonoSystem
        {
            // The GameObject that the MonoSystem MonoBehavior component
            // lives on cannot be null. Throw an exception if it is.
            //
            // If this happens, you probably forgot to add your child MonoSystem
            // GameObject to its serialized field in the GameManager prefab.
            if (monoSystem == null)
            {
                throw new NullReferenceException($"{nameof(monoSystem)} cannot be null");
            }

            // If the GameObject for the MonoSystem isn't null:
            // - Grab the overlying type from the MonoSystem's interface.
            // - Add <Type, IMonoSystem> pair to the IDictionary of MonoSystems.
            var monoSystemType = typeof(TBindTo);
            monoSystems[monoSystemType] = monoSystem;
        }

        // When getting a MonoSystem from the GameManager singleton, all
        // that is required is specifying the type of MonoSystem you are
        // trying to get.
        public TMonoSystem GetMonoSystem<TMonoSystem>()
        {
            // First we get the overlying type of the requested MonoSystem.
            var monoSystemType = typeof(TMonoSystem);
            
            // We then check the IDictionary of MonoSystems for this type of MonoSystem.
            if (monoSystems.TryGetValue(monoSystemType, out var monoSystem))
            {
                // If this type of MonoSystem exists in the IDictionary, we return
                // it as a TMonoSystem : IMonoSystem.
                return (TMonoSystem)monoSystem;
            }

            // If the type of MonoSystem was not found in our IDictionary of MonoSystems,
            // we throw an Exception.
            //
            // Typically when we run into this issue, it is because we forgot to
            // drag and drop the child GameObject associated with the MonoSystem into
            // the serialized field on the GameManager prefab.
            throw new Exception($"MonoSystem {monoSystemType} does not exist");
        }
    }
}
```

## State Machines

In the heart of the Akashic core architecture we have `StateMachines`. These constructs are used for orchestrating the distinct phases, behaviors, or operational modes of various game components. Their intricate design allows for a robust and type-safe approach to manage `state` transitions without leaning on less robust methods such as `enums`. Let's take an in-depth look into how `StateMachines` are implemented in our core architecture:

### IState

The `IState` `interface` offers generic typing of specific `state` types, tailored for each individual `StateMachine`. By returning its type, it ensures type-safety and clear differentiation between `states` belonging to specific `StateMachines`.

```csharp
using System;

namespace Akashic.Core.StateMachines
{
    // The IState interface is used to generically type all
    // States, and is the beginning of our class-based State
    // definitions for individual StateMachines.

    /// <summary>
    /// For generic typing of specific state types on a per-State Machine basis.
    /// Returns the type of IState for type safety.
    /// </summary>
    internal interface IState
    {
        Type GetFiniteStateType();
    }
}
```

### IFiniteState

In tandem with `IState`, the `IFiniteState` `interface` takes a more granular approach. It is designed to specify `finite states` within an `IState`. Through this `interface`, we eliminate the need for `enums` and instead rely on class-based `state` definitions, again ensuring type-safe operation.

```csharp
using System;

namespace Akashic.Core.StateMachines
{
    // IFiniteStates allow us to define specific finite states within an IState
    // using class-based definitions for IFiniteStates rather than enums.
    
    /// <summary>
    /// Allows us to define specific finite states within an <see cref="IState"/>.
    /// This enables us to define state machines without enums.
    /// Interface returns the type of <see cref="IFiniteState"/> for type safety.
    /// </summary>
    internal interface IFiniteState
    {
        Type GetStateType();
    }
}
```

### StateChangedMessage

`Message Events` are essential for communicating `state` changes within our individual `StateMachines`. The `StateChangedMessage` serves as a specialized `Message Event` for any `state` changes, and establishes a specific pattern for `state` transitions that is uniform across the game, ensuring clarity and reducing chances unexpected game behaviour.

```csharp
using Akashic.Core.Messages;

namespace Akashic.Core.StateMachines
{
    // When defining a StateChangedMessage Event, we must inherit
    // from StateChangedMessage class specifically.
    
    /// <summary>
    /// Base Message Event for State Changes.
    /// Enforces specific state change pattern across the game.
    /// </summary>
    internal class StateChangedMessage<TFiniteState> : IMessage where TFiniteState : IFiniteState
    {
        // It is very important that we specify the previous and next states
        // for our StateChangedMessage. Using other states violates our
        // state change pattern and may lead to unexpected game behaviour.
        public TFiniteState PrevState { get; protected set; }
        public TFiniteState NextState { get; protected set; }
        
        public StateChangedMessage() { }
    }
}
```

### BaseStateMachine

The `BaseStateMachine` class functions as the backbone for the `MonoBehaviour` companion script for our `StateMachines`. Through it, we manage `state` transitions, establish initial `states`, and propagate messages about these transitions. Its design enforces a specific `state` change pattern, which guarantees that the game responds uniformly to `state` changes.

```csharp
using System;
using Akashic.Core.Messages; // We use the Core.Messages namespace to create a StateChangedMessage.
using UnityEngine;

namespace Akashic.Core.StateMachines
{
    // All StateMachines will inherit from our BaseStateMachine abstract class.
    internal abstract class BaseStateMachine : MonoBehaviour, IStateMachine
    {
        // All StateMachines must have fields for the current state and a previous state.
        private IFiniteState currentState;
        private IFiniteState prevState;
        
        // The SetState(IFiniteState nextState) method is used to enforce adherence 
        // of a state change pattern within a particular StateMachine. 

        /// <summary>
        /// Sets the next state of the State Machine and publishes a State Changed Message.
        /// Used to enforce specific state change pattern across the game.
        /// </summary>
        /// <param name="nextState"></param>
        protected void SetState(IFiniteState nextState)
        {
            if (nextState == null)
            {
                // If the next state is null, throw an Exception.
                // If you get this, something has gone wrong.
                throw new Exception("Next state is null.");
            }

            if (currentState == nextState)
            {
                // If we are already in the next state, we should log a warning.
                // This may indicate that something in the game is not behaving as expected.
                Debug.LogWarning($"State Machine is already in \"{nextState}\" state.");
                return;
            }
            
            if (currentState != null && currentState.GetStateType() != nextState.GetStateType())
            {
                // If we have somehow received a state that is not the same type as the IState 
                // for a particular StateMachine, we throw an Exception.
                throw new Exception($"Invalid state transition from \"{currentState}\" to \"{nextState}\".");
            }

            // Set the previous state to the current state.
            prevState = currentState;
            
            // Create a StateChangedMessage Event and use the previous state and next state as parameters,
            // then publish the StateChangedMessage Event.
            var stateChangedMessage = CreateStateChangedMessage(prevState, nextState);
            GameManager.Publish(stateChangedMessage);
            
            // Now that the StateChangedMessage Event has been published, we can
            // set the current state to the next state.
            currentState = nextState;
            
            // This is only here for console based debugging purposes.
            Debug.Log($"State Machine is now in \"{nextState}\" state.");
        }
        
        // Additionally, all StateMachines must implement a method for setting the initial state.
        // This should be called during Awake().

        /// <summary>
        /// Sets the initial state of the State Machine.
        /// Must be called during Awake().
        /// </summary>
        protected abstract void SetInitialState();
        
        // Lastly, all StateMachines must implement a method for creating a StateChangedMessage Event.

        /// <summary>
        /// Creates a State Changed Message while enforcing adherence of a state change pattern
        /// that communicates specifically <paramref name="prevState"/> and <paramref name="nextState"/>.
        /// </summary>
        protected abstract IMessage CreateStateChangedMessage(IFiniteState prevState, IFiniteState nextState);
    }
}
```

### IStateMachine

Much like its sibling `interfaces`, the `IStateMachine` `interface` ensures generic typing for specific `StateMachines`. The use of generic typing accomplishes much the same goal as it does for our `MonoSystems` in that it allows us to leverage the service locator pattern to achieve clean dependency injection of a specific `StateMachine` without the need for tight coupling.

```csharp
namespace Akashic.Core.StateMachines
{
    // The IStateMachine interface is used to generically type all
    // StateMachines so that regardless of the overlying type
    // of the StateMachine, they can be bootstrapped into the
    // GameManager at runtime.
    
    /// <summary>
    /// For generic typing of specific state machines.
    /// Allows us to use the MonoSystem locator pattern in
    /// the <see cref="GameManager"/> to get a specific state machine.
    /// </summary>
    internal interface IStateMachine
    {
    }
}
```

### State Machine Manager

The `StateMachineManager` plays a pivotal role in the Akashic core architecture by managing `StateMachine` instances, and providing access to them through the service locator pattern for clean dependency injection. It provides methods to add and retrieve `StateMachines`, and much like the `MessageManager` and `MonoSystemManager`, it uses an `IDictionary` to map `StateMachine` types to their instances, simplifying dependency injection and making them freely available throughout our `GameManager`.

```csharp
using System;
using System.Collections.Generic;

// The StateMachineManager allows us to bootstrap StateMachines generically typed
// via IStateMachine by using the AddStateMachine() method. It also allows us to
// inject dependencies on StateMachines in other scripts by using GetStateMachine().
namespace Akashic.Core.StateMachines
{
    // This class is internal to the Core.MonoSystems namespace, and sealed to prevent 
    // extension through inheritance.
    internal sealed class StateMachineManager
    {
        // Create an IDictionary to pair each IStateMachine with its type.
        private readonly IDictionary<Type, IStateMachine> stateMachines =
            new Dictionary<Type, IStateMachine>();

        // Bootstrap a StateMachine into the game at runtime. This is called in
        // OnInitialized() in the AkashicGameManager.
        //
        // When bootstrapping a StateMachine, we grab the StateMachine's companion
        // MonoBehaviour class as a component from a GameObject.
        // The GameObject that the StateMachine MonoBehaviour class resides on
        // should be parented to the StateMachines child GameObject of our
        // GameManager prefab (found in resources).
        //
        // We then bind the TMonoSystem : IStateMachine to its companion
        // MonoBehaviour class.
        public void AddStateMachine<TState, TBindTo>(TState stateMachine)
            where TState : TBindTo, IStateMachine
        {
            // The name of the GameObject that the StateMachine MonoBehavior component
            // lives on cannot be null. Throw an exception if it is.
            //
            // If this happens, you probably forgot to add your child StateMachine
            // GameObject to its serialized field in the GameManager prefab.
            if (stateMachine == null)
            {
                throw new NullReferenceException($"{nameof(stateMachine)} cannot be null");
            }

            // If the GameObject for the StateMachine isn't null:
            // - Grab the overlying type from the StateMachine's interface.
            // - Add <Type, IStateMachine> pair to the IDictionary of StateMachines.
            var stateType = typeof(TBindTo);
            stateMachines[stateType] = stateMachine;
        }

        // When getting a StateMachine from the GameManager singleton, all
        // that is required is specifying the type of StateMachine you are
        // trying to get.
        public TState GetStateMachine<TState>()
        {
            // First we get the overlying type of the requested StateMachine.
            var stateMachineType = typeof(TState);
            
            // We then check the IDictionary of MonoSystems for this type of StateMachine.
            if (stateMachines.TryGetValue(stateMachineType, out var stateMachine))
            {
                // If this type of StateMachine exists in the IDictionary, we return
                // it as a TStateMachine : IStateMachine.
                return (TState)stateMachine;
            }

            // If the type of StateMachine was not found in our IDictionary of StateMachines,
            // we throw an Exception.
            //
            // Typically when we run into this issue, it is because we forgot to
            // drag and drop the child GameObject associated with the StateMachine into
            // the serialized field on the GameManager prefab.
            throw new Exception($"State Machine {stateMachineType} does not exist");
        }
    }
}
```

## Akashic Game Manager

The `AkashicGameManager` is our final stop in our architectural deep-dive. The `AkashicGameManager` script inherits from our base `GameManager` class and provides a front facing API for developers to use in order to add new `StateMachines`, and `MonoSystems` to the game's bootstrapping routine. All of our core architectural dependencies live on `GameObjects`, and as such the `AkashicGameManager` is added as a script component to the `GameManager` prefab, which lives at the root of our "Resources" folder. Let's take a closer look at the `AkashicGameManager` script and how it is used:

```csharp
using Akashic.Core;
using UnityEngine;

// The AkashicGameManager script is a component of the root GameObject
// on the GameManager prefab in Assets/Resources.
//
// This script contains references to the parent Transforms of the StateMachines,
// MonoSystems, and Controllers 
// These parent Transforms are used for controlling the timing of when
// their Unity Lifecycle methods are executed, and ensures that none of them will
// attempt to get MonoSystems and so on, or broadcast Message Events prior 
// to the completion of game bootstrapping.
namespace Akashic.Runtime
{
    // This class is internal to the Core namespace and sealed to prevent
    // extension via inheritance.
    //
    // We inherit from GameManager, which takes care of the logic for:
    // - Instantiation of this Singleton prior to the scene loading.
    // - Adding/removing/publishing Message Events.
    // - Adding/removing/getting StateMachines, and MonoSystems.
    // - Giving us access to various overrides related to game bootstrapping.
    internal sealed class AkashicGameManager : GameManager
    {
        // All parent transforms used for individual StateMachines, MonoSystems,
        // and Controllers are organized under the "Management" header.
        //
        // For every StateMachine, or MonoSystem created for the game:
        //
        // - Create a child GameObject under the associated ParentTransform in the prefab
        // - Serialize a reference to the GameObject's MonoBehaviour StateMachine,
        //   or MonoSystem component into the editor.
        // - Drag and drop the child GameObject to the appropriate serialized field
        [Header("Management")]
        [SerializeField] private Transform statesParentTransform;
        
        [SerializeField] private Transform monoSystemsParentTransform;
        
        [SerializeField] private Transform entitySystemsParentTransform;

        // We use the controllersParentTransform purely for timing.
        // This prevents any Controllers on the GameManager prefab
        // from interacting with a StateMachine, or MonoSystem
        // before bootstrapping is complete.
        [SerializeField] private Transform controllersParentTransform;
        
        // We should keep our StateMachines, MonoSystems, and Controllers
        // organized under their own headers.
        //
        // Since we are referencing the MonoBehaviour script component from the GameObject
        // associated with our StateMachines, and MonoSystems, we
        // serialize their MonoBehaviour script components into the editor.
        [Header("State Machines")]
        [SerializeField] private ExampleStateMachine exampleStateMachine;
        
        [Header("MonoSystems")]
        [SerializeField] private ExampleMonoSystem exampleMonoSystem;
        
        // Note that we do not need to serialize references to individual Controllers.
        
        // At runtime and after instantiation, we set the name of the instantiated
        // GameManager prefab to be the same as this script component.
        // In our case, it will be name AkashicGameManager.
        protected override string GetGameName()
        {
            return nameof(AkashicGameManager);
        }

        // OnInitialized() is called after the singleton GameManager
        // prefab has been instantiated into the scene. This is where
        // the bootstrapping happens.
        protected override void OnInitialized()
        {
            // We separate bootstrapping of individual StateMachines, and MonoSystems
            // into their own methods.
            InitializeGameStateMachines();
            InitializeGameMonoSystems();
            
            // Once bootstrapping is complete, we set the parent GameObjects
            // of the StateMachines, MonoSystems, and Controllers 
            // to active, which allows them to begin executing
            // their Unity Lifecycle methods.
            SetParentsActive();
        }

        // InitializeGameStateMachines() is where we add
        // StateMachines to the StateMachineManager so that we can
        // get them from the GameManager by calling 
        // GameManager.GetStateMachine<T>().
        protected override void InitializeGameStateMachines()
        {
            // When adding a StateMachine, or MonoSystemto the associated Manager
            // within the GameManager, we must bind the MonoBehaviour script component
            // to its associated interface. We also must specify that we are targeting the
            // GameObject associated with the MonoBehaviour script component.
            AddStateMachine<GameStateMachine, IGameStateMachine>(gameStateMachine);
        }
        
        // InitializeGameMonoSystems() is where we add
        // MonoSystems to the MonoSystemManager so that we can
        // get them from the GameManager by calling 
        // GameManager.GetMonoSystem<T>().
        protected override void InitializeGameMonoSystems()
        {
            AddMonoSystem<ExampleMonoSystem, IExampleMonoSystem>(exampleMonoSystem);
        }

        // SetParentsActive() is called after bootstrapping is complete.
        protected override void SetParentsActive()
        {
            statesParentTransform.gameObject.SetActive(true);
            monoSystemsParentTransform.gameObject.SetActive(true);
            entitySystemsParentTransform.gameObject.SetActive(true);
            controllersParentTransform.gameObject.SetActive(true);
        }
    }
}
```

# Conclusion

The `CoreArchitecture.md` document lays the technical foundation for building a robust, scalable, and flexible enterprise-level game. By exploring the mechanics behind core components such as `Message Events`, `StateMachines`, `MonoSystems`, and how these are managed by and retrieved from the `GameManager`, developers can gather a greater understanding of how our core architecture works under the hood. This structured approach simplifies navigation and comprehension of the codebase, while fostering an environment that's conducive to collaborative development and rapid feature iteration and implementation. By leveraging the outlined architectural principles, game development can be accelerated, giving developers greater agility and delivering a seamless experience for players.
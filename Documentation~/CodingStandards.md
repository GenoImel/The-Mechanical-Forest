# Coding Standards

This document provides a quick-start guide for working within the standards and the core architecture for the game. Templates with heavy commentary are provided to empower developers to begin working with the codebase with short suspense. The `CodingStandards.md` document does not, however, describe the game on a deep architectural level. For more information concerning the architectural structure of the codebase, please see `CoreArchitecture.md`.

In general, the coding style should follow the C# Coding Conventions as defined by [Microsoft](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions). These coding standards should make our lives easier by ensuring the overall readability and maintainability of the code. Note that we also follow the SOLID programming principles and try to adhere to the Gang of Four design patterns whenever possible to enable flexibility and scalability of the codebase as it grows. See these resources ([SOLID Part 1](https://www.youtube.com/watch?v=QDldZWvNK_E&t=173s&pp=ygUWc29saWQgdW5pdHkgaW5mYWxsaWJsZQ%3D%3D), [SOLID Part 2](https://www.youtube.com/watch?v=Fs8jy7DHDyc&t=1s&pp=ygUWc29saWQgdW5pdHkgaW5mYWxsaWJsZQ%3D%3D), [SOLID Unity - Unity 2017](https://www.youtube.com/watch?v=eIf3-aDTOOA&t=14s&pp=ygUWc29saWQgdW5pdHkgaW5mYWxsaWJsZQ%3D%3D)) for more information on the SOLID programming principles, and visit [Refactoring Guru](https://refactoring.guru/) for a quick overview on the Gang of Four design patterns, along with general philosophies and techniques for writing clean, maintainable, and scalable code.

## File Structure

Recommendations for the file/folder structure of a Unity project is documented below. Through the development cycle of the game, we may have small deviations from the structure that is outlined. If the need arises, this section will be updated to reflect any changes.

```text
// All core architectural scripts are placed here
// The GameManager resides at the root of Core,
// while individual architectural modules are organized into subfolders.
Assets/Scripts/Core

// All Runtime game code resides here, separated by categorical subfolders
Assets/Scripts/Runtime

// As an example, an individual MonoSystem, State Machine, or Controller
// Would be segregated into subfolders within the following category folders:
Assets/Scripts/Runtime/Controllers
Assets/Scripts/Runtime/MonoSystems
Assets/Scripts/Runtime/StateMachines

// Editor scripts which can also reference Runtime scripts live here
Assets/Scripts/Editor

// Code for unity tests and test-driven development will be placed here
Assets/Scripts/Tests/Editor
Assets/Scripts/Tests/Runtime

//All game config settings go here, organized by subfolder
Assets/Settings

//Rendering settings go here, specifically
Assets/Settings/Rendering

// GameManager.prefab resides at the root of the Resources Folder
// Further subfolders within the Resources folder can be used to organize
// any Scriptable Objects used in the game
Assets/Resources

// All prefabs are kept in the Assets/Prefabs folder, and organized within subfolders
// which mirror how we organize the Runtime folder.
Assets/Prefabs/Controllers
Assets/Prefabs/MonoSystems
Assets/Prefabs/StateMachines

// Plugins folder should only contain third party code and assets
Assets/Plugins

// Third party code supported by the Unity package manager is placed here
Packages

// Animation clips and controllers organized into subfolders
Assets/Animations

// Materials and font asset materials organized into subfolders
Assets/Materials

// All scenes for the game organized into subfolders
Assets/Scenes

// Custom shader code and shader graphs organized into subfolders
Assets/Shaders

// UI sprites and images go here organized into subfolders
Assets/Textures

// If any audio files are needed, these will go here and be organized into subfolders
Assets/Sound
```

Project file and folder structure is organize such that we follow industry best practices, ensuring a clean and navigable codebase. Core scripts, runtime game code, and scriptable objects have been distinctly categorized, with third-party elements kept separate for clarity. Assets, whether animations, materials, scenes, or shaders, are systematically arranged, which maintains an organized an intuitive project environment. This structured approach aids in both development efficiency and ease of collaboration.

## General

A general script is one that is used for controllers and other dynamic, feature restricted scripts. These can either be preloaded by the `GameManager` in cases where they are needed across different scenes, or they may be placed into a specific scene if their functionality is limited in architectural scope. The general format for a `MonoBehaviour` Controller or other dynamic script is as follows:

```csharp
// We should try to keep our namespaces organized for readability and maintainability.
// Organization is not strict, but generally we should organize them as follows:
//
using System; // References to external namespaces are added at the top.
using System.Collections.Generic;
using System.Linq;
using UnityEngine; // References to Unity internal namespaces are added next.
using UnityEngine.UI; 
using UnityEngine.InputSystem;
using Akashic.Core; // References to namespaces internal to the game are added last.

// All scripts must have a namespace:
// - Namespaces should have Akashic as the root
// - Namespaces should always match the folder/filepath location of the script
namespace Akashic.Runtime.Example
{
    // Most classes are marked as internal to restrict visibility to this namespace only.
    // Other classes outside of the namespace will need to import the namespace with a using statement.
    // Unless inheritance is required, we mark classes as sealed to end the line of inheritance.
    internal sealed class ExampleMonoBehavior : MonoBehaviour
    {
        // Editor fields are the front-end of MonoBehaviour scripts, and are defined first.
        // Fields should be grouped using Header attributes to ease readability in both the script and editor
        //
        // If the project contains Odin Inspector as a package, you can use FoldoutGroups instead.
        // Just don't mix both Headers and FoldoutGroups in the same class!
        [Header("Buttons")]
        [SerializeField] 
        private Button exampleButton;
        
        [Header("UI")]
        [SerializeField] 
        private Image exampleImage;

        // When using input from a keyboard or controller, we should always serialize references to input
        // in the editor to avoid hard-coding.This makes reconfiguring inputs less of a headache.
        [Header("Player Input")] 
        [SerializeField]
        private InputActionReference inputButton;

        // When using numeric values:
        // - Try to restrict invalid values.
        // - If possible, try to assign a default value so prefabs can be reset.
        [Header("Configuration")] 
        [Min(0f)] 
        [SerializeField] 
        private float speed = 10f; 
        
        // Constants and static fields can be treated as regular class fields.
        // These are placed after editor fields.
        private const int MaxPoints = 10;

        // We place all regular fields after static and constant fields.
        //
        // When defining regular fields:
        // - All regular fields should be private
        // - Properties should be used to expose fields where needed
        //
        // Additionally, we should restrict these fields as much as possible:
        // - ICollection as no List methods are used
        // - readonly as we never re-assign this field.
        private readonly ICollection<Vector3> points = new List<Vector3>();
        
        // Properties exposing private fields are defined following the fields they expose.
        // These should be as restrictive as possible:
        // - IEnumerable is used when accessing an ICollection in this example
        public IEnumerable<Vector3> Points => points;
        
        // We define all Unity and C# actions, events, and delegates after back-end fields and properties.
        //
        // Unity Actions and C# events require tight coupling between the classes invoking them
        // and the classes that respond to them. Therefore, Unity Actions and C# events are
        // restricted to MonoBehaviour classes that reside on the same prefab GameObject.
        public event Action OnPointAdded;
        
        // References to MonoSystems are private and placed at the end.
        private IExampleMonoSystem exampleMonoSystem;
        
        // Unity MonoBehaviour lifecycle methods are placed after all fields, properties, actions,
        // and dependencies on MonoSystems. The order of the LifeCycle methods should mirror the order
        // documented in Unity's execution order documentation.
        //
        // These methods must always be private. If inheritance was used, then they should be
        // protected virtual. This isn't a recommended practice, but is sometimes necessary.
        private void Awake()
        {
            // Awake is executed once, before the GameObject is enabled and prior to the first frame.
            // - Used to initialize variables only.
            // - No computations should be done here.
            // - Also used for dependency injection of MonoSystems.
            exampleMonoSystem = GameManager.GetMonoSystem<IExampleMonoSystem>();
        }
        
        // OnEnable and OnDisable methods are used for event subscription. When adding listeners in OnEnable(),
        // we should always remember to remove them in OnDisable().
        // Remembering to do this will reduce computation on disabled components.
        //
        // If you find that you are adding/removing many listeners, we will wrap these in
        // methods called AddListeners() and RemoveListeners(). This will keep the script organized.
        // Let's show an example here...
        private void OnEnable()
        {
            // Call AddListeners() in OnEnable().
            AddListeners();
        }
        
        private void OnDisable()
        {
            // Remember to remove listeners in OnDisable().
            RemoveListeners();
        }
        
        // Start is similar to Awake, but executes in the first frame and after OnEnable().
        // - Computations can be done here.
        private void Start()
        {
            DoPrivateThings();
        }
        
        // Following Unity Lifecycle methods, we define public, protected, and private methods,
        // ideally in that order for organization purposes.
        
        /// <summary>
        /// We should also try to add summaries to public methods using xml tags.
        /// Usage of xlm summaries will display this information when you mouse over
        /// the method in another script.
        /// </summary>
        public void DoPublicThings()
        {
            // Long code lines should be avoided. Aim for 100/120 columns when possible.
            // This will improve readability during PRs.
            var firstPoint = points
                .Where(point => point.x == 0f && point.y == 0f && point.z <= 0)
                .FirstOrDefault();
        }
        
        /// <summary>
        /// Remember to include a short summary for your public methods.
        /// APIs are much nicer to work with when they are self-documenting!
        /// </summary>
        public void DoPublicThings(
            string arg1,
            string arg2,
            string arg3,
            string arg4,
            string arg5,
            string arg6,
            string arg7
        )
        {
            // All methods with multiple arguments which exceed 100/120 columns should be split
            // into multiple lines. This improves general readability of class method.
        }
        
        private void OnAddPointButtonClicked()
        {
        }
        
        private void DoPrivateThings()
        {
        }

        // When defining a private response method for a message received over the message bus
        // we must include the message in the method's signature.
        private void OnExampleMessageReceived(ExampleMessage message)
        {
            //do stuff with the ExampleMessage.
        }
        
        // If we have AddListeners() or RemoveListeners() methods, these are placed 
        // at the bottom of the script.
        // We use these methods to consolidate adding and remove listeners to the script,
        // and then call AddListeners() in OnEnable, and RemoveListeners() in OnDisable().
        private void AddListeners()
        {
            //We add listeners for all Unity and C# actions, events, and delegates first.
            addPointButton.onClick.AddListener(OnAddPointButtonClicked);
            
            // If we are listening for player input from a keyboard, mouse, or controller, 
            // we add the listeners after events.
            inputButton.action.performed += OnInputButtonPressed;
            inputButton.action.Enable();
            
            // We can also leverage the core game architecture to listen for Message Events sent via message bus.
            // These listeners are added last.
            //
            // Make sure to include a private response method for the message.
            GameManager.AddListener<ExampleMessage>(OnExampleMessageReceived);
        }

        private void RemoveListeners()
        {
            // Consolidate removing listeners here and then call RemoveListeners in OnDisable().
            addPointButton.onClick.RemoveListener(OnAddPointButtonClicked);
            
            inputButton.action.performed -= OnInputButtonPressed;
            inputButton.action.Disable();
            
            GameManager.RemoveListener<ExampleMessage>(OnExampleMessageReceived);
        }
    }
}
```

By following the above conventions, we can emphasize a structured approach to `MonoBehaviour` scripts in Unity, focusing on organization and readability. Through a clear hierarchy from namespace imports to method declarations, along with practices like succinct code, comprehensive summaries, and effective event listener management, these guidelines promote a consistent and coherent coding style for streamlined development and collaboration.

## MonoSystems

`MonoSystems` are used to share data and code across `MonoBehaviour` controllers and other dynamic scripts without the need for tight coupling across the scene. All `MonoSystems` are bootstrapped into the core architecture at runtime by the `AkashicGameManager` prefab. For information on how to add a `MonoSystem` to the bootstrapping routine, please see the `CoreArchitecture.md` document.

When creating a new `MonoSystem`, we must first define an interface for the `MonoSystem` as shown below:

```csharp
using Akashic.Core.MonoSystems // Required for inheriting from IMonoSystem

// All MonoSystems require an interface to be defined.
//
// The interface of the MonoSystem resides in a subfolder of the same name within
// the the "Assets/Runtime/MonoSystems" folder.
//
// Defining an interface for our MonoSystem allows for MonoSystems to be generically typed,
// which enables the bootstrapping of MonoSystems at runtime. Interfaces also allow us
// to restrict how these MonoSystems are used by other classes.
//
// All public methods for the MonoSystem must have their method signatures
// defined in the interface itself.
namespace Akashic.Runtime.MonoSystems.ExampleMonoSystem
{
    /// <summary>
    /// We should always add summaries to interface class definitions.
    /// All MonoSystem interfaces must inherit from IMonoSystem for generic typing.
    /// </summary>
    internal interface IExampleMonoSystem : IMonoSystem
    {
        /// <summary>
        /// We should also always add summaries to interface methods.
        /// </summary>
        public void DoThings();
    }
}
```

After defining the interface for the `MonoSystem`, we create a `MonoBehaviour` companion class in another script. This script will be added as a component of a `GameObject` by the same name of the `MonoSystem`:

```csharp
using UnityEngine;
using Akashic.Core; // Required for any interaction with GameManager
// If you need access to any Message Events defined in another Controller/MonoSystem/etc. namespace, make sure to add it.
//
// Don't abuse this accessibility, however. Using other namespaces to get direct injection of a Controller
// or feature specific MonoBehaviour is not recommended within MonoSystems, States, or across other separate features.
// Doing so can lead to ugly instances of tight coupling.
using Akashic.Runtime.Example;

// The namespace of the MonoSystem should always match its file/folder location.
namespace Akashic.Runtime.MonoSystems.ExampleMonoSystem
{
    // MonoSystems are a key part of our game for the following reasons:
    // - MonoSystems minimize the number of singletons in a game's architecture.
    // - They enable code and data sharing across features.
    // - Dependency injection using GameManager.GetMonoSystem() minimizes tight-coupling
    //   across classes and GameObjects.
    //
    // Also note that when inheriting interfaces, we don't need to specify summaries. These summaries
    // are already inherited from the interface.
    
    // In general when writing the class definition for a MonoSystem:
    // - Mark the class as internal to restrict its visibility to the namespace it lives in.
    // - Make the class sealed to prevent further inheritance.
    // - Inherit from MonoBehaviour so that we can add the MonoSystem to a GameObject as a component.
    // - Inherit from the companion interface, in this case IExampleMonoSystem.
    internal sealed class ExampleMonoSystem : MonoBehaviour, IExampleMonoSystem
    {
        // When defining fields for MonoSystems, we follow the same general rules.
        // In general, however, MonoSystems should only contain regular private fields
        // and public properties for accessing those fields. See the example MonoBehaviour
        // script for coding standards concerning fields.
        //
        // MonoSystems should not contain Unity/C# actions, events, or delegates as these require
        // tight coupling. Use messages to broadcast events.

        private IOtherExampleMonoSystem otherExampleMonoSystem;

        // We can conveniently use Unity's lifecycle methods within our MonoSystems, since we
        // inherit from MonoBehaviour.
        private void Awake()
        {
            // We can also initialize dependencies.
            // This includes dependencies on other MonoSystems!
            otherExampleMonoSystem = GameManager.GetMonoSystem<IOtherExampleMonoSystem>();
        }

        private void OnEnable()
        {
            // We can Add listeners for messages in our MonoSystems as well.
            // Make sure to wrap these in AddListeners().
            AddListeners();
        }

        private void OnDisable()
        {
            // Make sure to use RemoveListeners() in OnDisable()!
            RemoveListeners();
        }

        // Summary is inherited from IExampleMonoSystem.
        public void DoThings()
        {
            DoPrivateThings();
        }
        
        // Private methods are used for logic internal to the MonoSystem, and defined after public methods.
        // Private methods should not be defined in the interface.
        private void DoPrivateThings()
        {
            // Do some calculations or some other private thing.
            // Note that you can publish messages over the message bus in a MonoSystem.
            var isEnabled = false;
            GameManager.Publish(new OtherExampleMessage(isEnabled));
        }

        // Make sure to wrap any listeners in AddListeners()/RemoveListeners()
        // This keeps our scripts nice and organized.
        private void AddListeners()
        {
            GameManager.AddListener<ExampleMessage>(OnExampleMessageReceived);
        }

        private void RemoveListeners()
        {
            // If you add listeners for messages broadcast over the message bus
            // remember to remove the same listeners in OnDisable().
            //
            // Also, if you find yourself adding and removing many listeners, wrap these in
            // methods called AddListeners() and RemoveListeners() to keep the script organized.
            GameManager.RemoveListener<ExampleMessage>(OnExampleMessageReceived);
        }
    }
```

In short, `MonoSystems` are essential components that facilitate the sharing of code and data across `MonBehaviour` `Controllers` and other dynamic scripts, promoting decoupled interactions between individual features. These are bootstrapped into the game at runtime by the `AkashicGameManagerPrefab`, and their creation involves defining specific `interfaces` and companion `MonoBehaviour` classes, ensuring modularity and minimizing direct dependencies in the game's architecture.

## States

Provided with the game architecture are core scripts for creating `States`, or finite state machines (FSMs). These state machines require the implementation of specific pieces of the core architecture in order to enforce adherence to a specific state change pattern, ensuring that `State Change Events` always happen in a specific order in every single FSM. Let's step through an `ExampleStateMachine` to better illustrate this, lets start by defining an overall `IState` definition specific to our `ExampleStateMachine`:

```csharp
// - System is needed for returning the typeof FiniteStates within our State definition.
//
// - Akashic.Core.StateMachines is required for inheriting from the IState interface.
//   This allows us to generically type our different States, enforcing type-safety
//   and allowing us to define State definitions with FiniteStates.
//
//   In short, we are creating class-based states, rather than using enums.
using System; 
using Akashic.Core.StateMachines; 

// The namespace of any scripts for a State Machine should always match its file/folder location.
namespace Akashic.Runtime.States.ExampleStates
{
    // All State definitions must inherit from the IState interface for type-safety.
    // This prevents us from mixing up different States from other State Machines.
    internal sealed class ExampleState : IState
    {
        // We must provide a method for returning the type of FiniteState that this State uses.
        // This enforces type-safety for our class-based States.
        public Type GetFiniteStateType()
        {
            return typeof(ExampleFiniteState);
        }
    }
}
```

Now that we have a parent type defined for our `IFiniteStates`, lets go on to define some class-based finite states for our `ExampleStateMachine`. To do this, create a separate script called `ExampleFiniteStates`:

```csharp
// - System is needed for returning the typeof State for an individual FiniteState.
//
// - Akashic.Core.StateMachines is required for inheriting from the IFiniteState interface.
//   This allows us to generically type our different IFiniteStates, enforcing type-safety
//   and allows us to define IFiniteState definitions without enums.
using System;
using Akashic.Core.StateMachines;

// The namespace of any scripts for a State Machine should always match its file/folder location.
namespace Akashic.Runtime.States.ExampleStates
{
    // All State definitions must inherit from the IFiniteState interface for type-safety.
    // This prevents us from mixing up different States from other State Machines.
    internal abstract class ExampleFiniteStates : IFiniteState
    {

        // We must provide a method for returning the type of State that this FiniteState belongs to.
        // This concludes the enforcement of type-safety for our State Machine
        // within our class-based State definition.
        public Type GetStateType()
        {
            return typeof(ExampleState);
        }
        
        // Now we can define some FiniteStates for our State Machine!
        // To do this we create some empty classes that inherit from our abstract FiniteState class.
        // Sealing these classes ends the inheritance chain.
        internal sealed class RedState : ExampleFiniteState
        {
        }
        
        internal sealed class BlueState : ExampleFiniteState
        {
        }
        
        internal sealed class GreenState : ExampleFiniteState
        {
        }
    }
}
```

Now we need to a create a `StateChangedMessage` that is specific to our `State Machine`'s `IState` and `IFiniteState` types for overall type-safety. This will allow us to communicate the `StateChangedMessage` `Event` without the risk of mixing types between different `State Machines`:

```csharp
// We need to use the Akashic.Core.StateMachines namespace to inherit from the StateChangedMessage<T> class.
using Akashic.Core.StateMachines;

// The namespace of any scripts for a State Machine should always match its file/folder location.
// 
// For our Message Events specifically, all Message Events must be placed in a script called Messages
// that resides within the namespace where these Message Events are most relevant.
// Our ExampleStateChangedMessage would be included in this individual Messages script.
namespace Akashic.Runtime.States.ExampleStates
{
    // To create an ExampleStateChangedMessage, we must inherit from the StateChangedMessage<T> class,
    // and provide the type of FiniteState that we are using.
    //
    // This Message, like all other Message Events in our game, should be sealed to end
    // the inheritance chain.
    internal sealed class ExampleStateChangedMessage : StateChangedMessage<ExampleFiniteState>
    {
        // As with all non-empty Message Events, we must provide a public constructor. For our StateChangedMessage<T>
        // Events specifically, we will want to provide parameters of the previous and next FiniteStates, and
        // pass them to the base constructor.
        //
        // It is very important to provide the previous and next states in this specific order as method parameters.
        // Mixing them up may cause unintended behaviour.
        public ExampleStateChangedMessage(ExampleFiniteState prevState, ExampleFiniteState nextState)
        {
            PrevState = prevState;
            NextState = nextState;
        }
    }
}
```

We can now get started on creating the `State Machine` itself. First we need to create the `IExampleStateMachine` `interface`:

```csharp
// We must use the Akashic.Core.StateMachines namespace to inherit from IStateMachine.
using Akashic.Core.StateMachines;

// All State Machines require an interface to be defined.
//
// The interface of the State Machines resides in a subfolder of the same name within
// the "Assets/Runtime/States" folder.
//
// Defining an interface for our State Machine allows for State Machines to be generically typed,
// which enables the bootstrapping of State Machines at runtime. Interfaces also allow us
// to restrict how these State Machines are used by other classes.
//
// All public methods for the State Machines must have their method signatures
// defined in the interface itself.
namespace Akashic.Runtime.States.ExampleStates
{
    /// <summary>
    /// We should always add a summary to the interface class definition.
    /// This State Machine sets states based on a panel's color, as an example.
    /// </summary>
    internal interface IExampleStateMachine : IStateMachine
    {
        /// <summary>
        /// We should also always add summaries to interface methods.
        /// As an example: Sets the red state.
        /// </summary>
        public void SetRedState();

        /// <summary>
        /// Sets the blue state.
        /// </summary>
        public void SetBlueState();

        /// <summary>
        /// Sets the green state.
        /// </summary>
        public void SetGreenState();
    }
}
```

Any time we create a `State Machine`, we also need a `MonoBehaviour` companion script component that we can place on a `GameObject`. This looks a little different than how `MonoSystems` did in our previous examples, because our `MonoBehaviour` inheritance is built into the `BaseStateMachine` class. This is done so that we can enforce specific methods to be used within any `State Machine` class, and comply with a specific state change pattern. Keeping all this in mind, here is our example companion script component for our `ExampleStateMachine`:

```csharp
// - AkashicSpace.Core.Messages is needed in order to create a new StateChangedMessage.
// - AkashicSpace.Core.StateMachines is needed in order to inherit from the BaseStateMachine class.
using Akashic.Core;
using Akashic.Core.Messages;
using Akashic.Core.StateMachines;
using Akashic.Runtime.States.GameStates; // We can also reference other State Machines.

// The namespace of any scripts for a State Machine should always match its file/folder location.
namespace Akashic.Runtime.States.ExampleStates
{
    // In general when writing the class definition for a State Machine:
    // - Mark the class as internal to restrict its visibility to the namespace it lives in.
    // - Make the class sealed to prevent further inheritance.
    // - Inherit from BaseStateMachine, which contains MonoBehaviour, so that we can add the
    //   State Machine to a GameObject as a component. This also enforces the use of specific abstract
    //   methods that are required for a StateChanged Event to work properly.
    // - Inherit from the companion interface, in this case IExampleStateMachine.
    internal sealed class ExampleStateMachine : BaseStateMachine, IExampleStateMachine
    {
        // We must use the MonoBehaviour Lifecycle method, Awake(), and use it to set the initial state.
        private void Awake()
        {
            // The use of this method is enforced by the BaseStateMachine class.
            SetInitialState();
        }

        // We can use OnEnable and OnDisable to listen to Message Events published over the Message Bus.
        // These can be from any other script in the game, including other State Machines.
        // 
        // For housekeeping purposes, we will want to wrap any listeners we Add/Remove in 
        // OnEnable()/OnDisable() with a call to AddListeners() and RemoveListeners() respectively.
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        // We must implement all public methods defined in our IExampleMonoSystem interface.
        // We'll start with RedState(). Here we will set the state of our ExampleStateMachine to the RedState.
        // All the logic for doing so is handled internally and privately by the BaseStateMachine class.
        public void SetRedState()
        {
            // Inside the public method, we will call the SetState method from the BaseStateMachine class.
            // Make sure to hand it an IFiniteState that belongs to ExampleState, or else you'll get an Exception.
            SetState(new ExampleFiniteState.RedState());
        }

        // Remember that public method summaries are inherited from the interface.
        public void SetBlueState()
        {
            // We do the same for all other publicly available methods for our interface.
            SetState(new ExampleFiniteState.BlueState());
        }

        public void SetGreenState()
        {
            SetState(new ExampleFiniteState.GreenState());
        }

        // The use of this method is enforced by the BaseStateMachine class. We must override it,
        // and define the initial state of the State Machine.
        //
        // We then call SetInitialState() in Awake() to ensure this gets set at Runtime.
        protected override void SetInitialState()
        {
            currentState = new ExampleFiniteState.RedState();
        }

        // The use of this method is enforced by the BaseStateMachine class. We must override it in order to 
        // return the specific StateChangedMessage that we created for our ExampleStateMachine.
        // In this case, we will be returning the ExampleStateChangedMessage that we created earlier.
        protected override IMessage CreateStateChangedMessage(IFiniteState prevState, IFiniteState nextState)
        {
            // We enforce that the constructor parameters for prevState and nextState
            // must be types of ExampleFiniteState.
            return new ExampleStateChangedMessage(
                prevState as ExampleFiniteState,
                nextState as ExampleFiniteState
            );
        }

        // Any response methods for Listeners must have the specific Message Event as a method parameter.
        // The return type is always void.
        private void OnGameStateChangedMessage(GameStateChangedMessage message)
        {
            // Because our states are class-based rather than enum-based, we must ask if the type of NextState
            // is the specific state we are looking for using an `is` comparison.
            if (message.NextState is GameFiniteState.LogoutState)
            {
                // If the NextState is the type of state we are looking for, we can call the SetRedState() method.
                // What we have done here is a light example of how we can leverage Hierarchical State Machines!
                //
                // This allows our State Machines to react to the state changes of other State Machines, creating a 
                // more flexible hierarchy of states.
                SetRedState();
            }
        }

        // Add any listeners to the Message Bus here, including Message Events from other State Machines.
        // Make sure to provide a response method!
        private void AddListeners()
        {
            GameManager.AddListener<GameStateChangedMessage>(OnGameStateChangedMessage);
        }

        private void RemoveListeners()
        {
            GameManager.RemoveListener<GameStateChangedMessage>(OnGameStateChangedMessage);
        }
    }
}
```

To summarize the above templates and commentary, `States` represent Finite State Machines (FSMs). They mandate specific implementation patterns to ensure that state transitions, known as `State Change Events`, occur in a designated sequence. Through the provided core scripts, the FSMs are designed to be class-based rather than reliant on enums, offering a more flexible and type-safe approach. This method of state management requires the use of `interfaces` and companion scripts such as `IState` and `IFiniteState`, to define and manage the FSMs. The sample provided, `ExampleStateMachine`, illustrates this by defining class-based states like `RedState`, `BlueState`, and `GreenState`. Additionally, `Message Events` like `StateChangedMessage` enable communication between different FSMs, allowing for a hierarchical structure of states. The use of `interfaces` and base classes, such as `BaseStateMachine`, ensure a consistent state change pattern across the game, whereas the use of `IStateMachine` facilitates runtime bootstrapping and the use of the service locator pattern through generic typing.

## Message Events

`Message Events` are a nice decoupled alternative to Unity/C# `actions`, `events`, and `delegates`. They allow for events to be communicated across the game by simply adding a listener for the `Message Event` in `OnEnable()`, and only require that the script listening for the `Message Event` uses `Akashic.Core` and the namespace that the `Messages` script lives in. 

When defining `Message Events` in the game, we create a `Messages` script that lives in the folder with the scripts that are most closely associated with publishing these messages. Here is a simple example for creating new `Message Events`:

```csharp
// When creating new Messages, we must use the Akashic.Core namespace.
// 
// This allows us to inherit from IMessage, which generically types the Message
// Event so that they can be added to the MessageManager in the core architecture.
using Akashic.Core;

// Messages scripts containing individual Message Events must live in a namespace that 
// corresponds to their file/folder location. These file/folder locations correspond
// to the feature most related to publishing the Message Events.
namespace Akashic.Runtime.Example
{
    // Message Events should be defined in separate files, and should be kept close to
    // their respective scripts.
    
    // Some quick notes on writing Message Events:
    // - May be empty, or contain small amounts of data.
    // - Must be marked as internal to restrict access within the namespace.
    // - Must be sealed to end the line of inheritance.

    // An example of an empty Message event.
    internal sealed class ExampleMessage : IMessage
    {
    }
    
    // An example of a Message event containing a small amount of data.
    internal sealed class OtherExampleMessage : IMessage
    {
        // Message events use properties to store data.
        // We allow scripts that receive these messages to get properties.
        // These properties can only be set inside a constructor.
        // This prevents other scripts from modifying the data after the 
        // Message event has been constructed prior to or after publishing.
        public bool IsEnabled { get; }

        // Message events that are not empty require a constructor in order
        // to assign values to their properties.
        public OtherExampleMessage(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }
    }
}
```

`Message Events` present a structured and efficient way to facilitate communication across various parts of the game, by offering a level of decoupling that enhances code maintainability and readability. `Message Events` are best suited for cross game communication. Note that we should only use Unity/C# `actions`, `events`, and `delegates` within a feature restricted scope (i.e. within the prefab hierarchy of a specific feature/Controller), and all other communication across the game should rely on `Message Events`.

# Conclusion

Adhering to this project's coding standards ensures a cohesive, efficient, and maintainable development environment. A meticulous approach to project organization makes our codebase navigable, which accelerates onboarding and collaborative productivity. Moreover, by leveraging core architectural components like `StateMachines`, `MonoSystems`, and `Message Events`, developers are empowered to craft scalable and modular features, enhancing both performance and flexibility. Abiding by these principles guarantees optimal code quality and robust game behaviour, benefiting both developers and players alike.

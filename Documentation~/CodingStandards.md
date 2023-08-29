# Coding Standards

In general, the coding style should follow the C# Coding Conventions as defined by [Microsoft](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions). These coding standards should make our lives easier by ensuring the overall readability and maintainability of the code. Note that we also follow the SOLID programming principles and try to adhere to the Gang of Four design patterns whenever possible to enable flexibility and scalability of the codebase as it grows.

This document provides a quick-start guide for working within the standards and the core architecture for the game. Templates with heavy commentary are provided to empower developers to begin working with the codebase with short suspense. The Coding Standards document does not, however, describe the game on a deep architectural level. For more information concerning the architectural structure of the codebase, please see `CoreArchitecture.md`.

## File Structure

Recommendations for the file/folder structure of a Unity project is documented below. Through the development cycle of the game, we may have small deviations from the structure that is outlined. If the need arises, this section will be updated to reflect any changes.

```
// All core architectural scripts are placed here
Assets/Scripts/Core

// All Runtime application code resides here
Assets/Scripts/Runtime

// Editor scripts which can also reference Runtime scripts live here
Assets/Scripts/Editor

// Code for unity tests and test-driven development will be placed here
Assets/Scripts/Tests/Editor
Assets/Scripts/Tests/Runtime

//All game config settings go here, organized by subfolder
Assets/Settings

//Rendering settings go here, specifically
Assets/Settings/Rendering

// Resources folder holds all application prefabs
// GameManager.prefab resides at the root Prefabs folder
// All other prefabs should be kept in appropriately named subfolders
Assets/Resources/Prefabs

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
```

## General

A general script is one that is used for controllers, managers, and other dynamic scripts that do not provide a MonoSystem. These can either be preloaded by the `GameManager` in cases where they are needed across different scenes, or they may be placed into a specific scene if their functionality is limited in architectural scope. The general format for a MonoBehaviour controller, manager, or other dynamic script is as follows:

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
    internal sealed class ExampleMonoBehaviour : MonoBehaviour
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
        // Remember to do this will reduce computation on disabled components.
        //
        // If you find that you are adding/removing many listeners, we will wrap these in
        // methods called AddListeners() and RemoveListeners(). This will keep the script organized.
        // Let's show that example here...
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
        // We must include the message in the method's signature.
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
            
            // We can also leverage the core application architecture to listen for messages sent via message bus.
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

## MonoSystems

`MonoSystems` are used to share data and code amongst `MonoBehaviour` controllers, managers, and other dynamic scripts without the need for tight coupling across the scene. All `MonoSystems` are bootstrapped into the core architecture at runtime by the `AkashicGameManager` prefab. For information on how to add a `MonoSystem` to the bootstrapping routine, please see the `CoreArchitecture.md` document.

When creating a new `MonoSystem`, we must first define an interface for the `MonoSystem` as shown below:

```csharp
// All MonoSystems require an interface to be defined.
//
// The interface of the MonoSystem resides in a subfolder of the same name within
// the "Assets/Runtime/MonoSystems" folder.
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
    /// </summary>
    internal interface IExampleMonoSystem
    {
        /// <summary>
        /// We should also always add summaries to interface methods.
        /// </summary>
        public void DoThings();
    }
}
```

After defining the interface for the `MonoSystem`, we create a `MonoBehavior` companion class in another script. This script will be added as a component of a `GameObject` by the same name of the MonoSystem:

```csharp
using UnityEngine;
using Akashic.Core; // Required for inheriting from IMonoSystem.
using Akashic.Runtime.Example;

// The namespace of the MonoSystem should always match it's file/folder location.
namespace Akashic.Runtime.MonoSystems.ExampleMonoSystem
{
    // MonoSystems allow us to share code and data across MonoBehaviour controllers, managers,
    // and other dynamic scripts without tight coupling. By using the GameManager singleton,
    // we can perform dependency injection to get access to MonoSystems using GameManager.GetMonoSystem().
    //
    // MonoSystems are a key part of our application for the following reasons:
    // - MonoSystems minimize the number of singletons in a game's architecture.
    // - They enable code and data sharing.
    // - Dependency injection minimizes tight-coupling across classes and GameObjects.
    //
    // Also note that when inheriting interfaces, we don't need to specify summaries. These summaries
    // are already inherited from the interface.
    
    // In general when writing the class definition for a MonoSystem:
    // - Mark the class as internal to restrict its visibility to the namespace it lives in.
    // - Make the class sealed to prevent further inheritance.
    // - Inherit from MonoBehavior so that we can add the MonoSystem to a GameObject as a component.
    // - Inherit from IMonoSystem for generic typing during the GameManager bootstrapping routine.
    // - Inherit from the companion interface, in this case IExampleMonoSystem.
    internal sealed class ExampleMonoSystem : MonoBehaviour, IMonoSystem, IExampleMonoSystem
    {
        // When defining fields for MonoSystems, we follow the same general rules.
        // In general, however, MonoSystems should only contain regular private fields
        // and public properties for accessing those fields. See the example MonoBehavior
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
            GameManager.AddListener<ExampleMessage>(OnExampleMessageReceived);
        }

        private void OnDisable()
        {
            // If you add listeners for messages broadcast over the message bus
            // remember to remove the same listeners in OnDisable().
            //
            // Also, if find yourself adding and removing many listeners, wrap these in
            // methods called AddListeners() and RemoveListeners() to keep the script organized.
            GameManager.RemoveListener<ExampleMessage>(OnExampleMessageReceived);
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
    }
```

## Message Events

`Message Events` are a nice decoupled alternative to Unity/C# actions, events, and delagates. They allow for events to be communicated across the application by simply adding a listener for the `Message` in `OnEnable()` or `OnDisable()`, and only require that the script listening for the message use `Akashic.Core` and the namespace that the `Messages` script lives in. 

When defining messages in the application, we create a `Messages` script that lives in the folder with the scripts that are most associated with publishing these messages. Here is a simple example for creating new `Message Events`:

```csharp
// When creating new Messages, we must use the Akashic.Core namespace.
// 
// This allows us to inherit from IMessage, which generically types the message
// so that they can be added to the MessageManager in the core architecture.
using Akashic.Core;

// Messages must live in a namespace that corresponds to their file/folder location.
namespace Akashic.Runtime.Example
{
    // Message bus events should be defined in separate files, and should be kept close to
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
        // Message event has been constructed prior to publishing.
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

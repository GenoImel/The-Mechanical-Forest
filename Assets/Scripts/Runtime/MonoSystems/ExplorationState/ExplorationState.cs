using UnityEngine;

namespace Akashic.Runtime.MonoSystems.ExplorationStates
{
    /// <summary>
    /// Holds certain states pertaining to the player's state while exploring.
    ///
    /// The current states are:
    ///
    /// Initializing
    /// Exploration
    /// Party
    /// Inventory
    /// None
    /// </summary>
    internal enum ExplorationState 
    {
        /// <summary>
        /// State conveying the scene is currently initializing
        /// </summary>
        Initializing,

        /// <summary>
        /// State conveying the player is exploring
        /// </summary>
        Exploration,

        /// <summary>
        /// State used when displaying the player's party
        /// </summary>
        Party,

        /// <summary>
        /// State used when displaying the player's inventory
        /// </summary>
        Inventory,

        /// <summary>
        /// General state used when no other situation applies
        /// </summary>
        None
    }
}

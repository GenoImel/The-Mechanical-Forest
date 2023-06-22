using UnityEngine;

namespace Akashic.Runtime.MonoSystems.BattleStates
{
    /// <summary>
    /// Used to publish changes in a battle's state
    ///
    /// The current states are:
    /// Initializing
    /// Loot
    /// Victory
    /// GameOver
    /// None
    /// </summary>
    internal enum BattleState 
    {
        /// <summary>
        /// Used when generating the initial battle scene
        /// </summary>
        Initializing,

        /// <summary>
        /// Used when displaying battle loot
        /// </summary>
        Loot,

        /// <summary>
        /// Used when the player characters win the battle
        /// </summary>
        Victory,

        /// <summary>
        /// Used when player character dies or otherwise the game ends
        /// </summary>
        GameOver,

        /// <summary>
        /// Used for anything not listed above
        /// </summary>
        None
    }
}

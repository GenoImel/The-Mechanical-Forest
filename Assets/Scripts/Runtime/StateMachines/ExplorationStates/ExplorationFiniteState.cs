using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.ExplorationStates
{
    internal class ExplorationFiniteState : IFiniteState
    {
        public Type GetStateType()
        {
            return typeof(ExplorationState);
        }

        /// <summary>
        /// Set when Exploration Scene is initializing.
        /// </summary>
        internal sealed class Initializing : ExplorationFiniteState
        {
        }
        
        /// <summary>
        /// Set when the player is exploring.
        /// </summary>
        internal sealed class Exploration : ExplorationFiniteState
        {
        }
        
        /// <summary>
        /// Set when the player is viewing their party.
        /// </summary>
        internal sealed class Party : ExplorationFiniteState
        {
        }
        
        /// <summary>
        /// Set when the player is viewing their inventory.
        /// </summary>
        internal sealed class Inventory : ExplorationFiniteState
        {
        }
        
        /// <summary>
        /// Set when the player is not in an Exploration Scene.
        /// </summary>
        internal sealed class None : ExplorationFiniteState
        {
        }
    }
}
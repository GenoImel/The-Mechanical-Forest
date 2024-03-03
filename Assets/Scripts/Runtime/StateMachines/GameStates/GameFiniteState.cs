using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.GameStates
{
    internal class GameFiniteState : IFiniteState
    {
        public Type GetFiniteStateType()
        {
            return typeof(GameFiniteState);
        }

        /// <summary>
        /// Set when player is in the Main Menu Scene.
        /// </summary>
        internal sealed class MainMenu : GameFiniteState
        {
        }

        /// <summary>
        /// Set when player is in the Exploration Scene.
        /// </summary>
        internal sealed class Exploration : GameFiniteState
        {
        }

        /// <summary>
        /// Set when player is in the Battle Scene.
        /// </summary>
        internal sealed class Battle : GameFiniteState
        {
        }
        
        /// <summary>
        /// Set when a story event is happening.
        /// </summary>
        internal sealed class Dialogue : GameFiniteState
        {
        }
        
        /// <summary>
        /// Set when the game is paused.
        /// </summary>
        internal sealed class Paused : GameFiniteState
        {
        }
    }
}
using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.BattleStates
{
    internal class BattleFiniteState : IFiniteState
    {
        public Type GetFiniteStateType()
        {
            return typeof(BattleFiniteState);
        }

        /// <summary>
        /// Used when generating the initial battle scene.
        /// </summary>
        internal sealed class Initializing : BattleFiniteState
        {
        }
        
        /// <summary>
        /// Used when the battle is in progress.
        /// </summary>
        internal sealed class BattleActive : BattleFiniteState
        {
        }
        
        /// <summary>
        /// Used when displaying battle loot.
        /// </summary>
        internal sealed class Loot : BattleFiniteState
        {
        }

        /// <summary>
        /// Used when the party wins a battle.
        /// </summary>
        internal sealed class Victory : BattleFiniteState
        {
        }
        
        /// <summary>
        /// Used when all party members die.
        /// </summary>
        internal sealed class GameOver : BattleFiniteState
        {
        }
        
        /// <summary>
        /// Used for anything not listed above.
        /// </summary>
        internal sealed class None : BattleFiniteState
        {
        }
    }
}
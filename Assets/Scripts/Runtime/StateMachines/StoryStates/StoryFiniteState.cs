using System;
using Akashic.Core.StateMachines;

namespace Akashic.Runtime.StateMachines.StoryStates
{
    internal class StoryFiniteState : IFiniteState
    {
        public Type GetStateType()
        {
            return typeof(StoryState);
        }

        /// <summary>
        /// Used when determining if a story event is currently inactive.
        /// </summary>
        internal sealed class Inactive : StoryFiniteState
        {
        }

        /// <summary>
        /// Used when determining if a story event is currently active.
        /// </summary>
        internal sealed class Active : StoryFiniteState
        {
        }
    }
}

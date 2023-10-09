using System;

namespace Akashic.Core.StateMachines
{
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
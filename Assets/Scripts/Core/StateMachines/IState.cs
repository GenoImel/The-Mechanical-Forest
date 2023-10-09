using System;

namespace Akashic.Core.StateMachines
{
    /// <summary>
    /// For generic typing of specific state types on a per-State Machine basis.
    /// Returns the type of IState for type safety.
    /// </summary>
    internal interface IState
    {
        Type GetFiniteStateType();
    }
}
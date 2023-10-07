using System;
using System.Collections.Generic;

namespace Akashic.Core.MonoSystems
{
    internal sealed class MonoSystemManager
    {
        private readonly IDictionary<Type, IMonoSystem> monoSystems =
            new Dictionary<Type, IMonoSystem>();

        public void AddMonoSystem<TMonoSystem, TBindTo>(TMonoSystem monoSystem)
            where TMonoSystem : TBindTo, IMonoSystem
        {
            if (monoSystem == null)
            {
                throw new Exception($"{nameof(monoSystem)} cannot be null");
            }

            var monoSystemType = typeof(TBindTo);
            monoSystems[monoSystemType] = monoSystem;
        }

        public TMonoSystem GetMonoSystem<TMonoSystem>()
        {
            var monoSystemType = typeof(TMonoSystem);
            if (monoSystems.TryGetValue(monoSystemType, out var monoSystem))
            {
                return (TMonoSystem)monoSystem;
            }

            throw new Exception($"MonoSystem {monoSystemType} does not exist");
        }
    }
}
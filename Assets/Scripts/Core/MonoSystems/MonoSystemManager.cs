using System;
using System.Collections.Generic;

namespace Akashic.Core.MonoSystems
{
    internal sealed class MonoSystemManager
    {
        private readonly IDictionary<Type, IMonoSystem> monoSystems =
            new Dictionary<Type, IMonoSystem>();
        
        public IDictionary<Type, IMonoSystem> MonoSystems => monoSystems;

        public void AddMonoSystem<TMonoSystem, TBindTo>(TMonoSystem monoSystem)
            where TMonoSystem : TBindTo, IMonoSystem
        {
            if (monoSystem == null)
            {
                throw new Exception($"{nameof(monoSystem)} cannot be null");
            }

            var monoSystemType = typeof(TBindTo);
            
            if (monoSystems.ContainsKey(monoSystemType))
            {
                throw new Exception($"Mono System of type {monoSystemType} already exists.");
            }
            
            monoSystems[monoSystemType] = monoSystem;
        }
        
        public void AddMonoSystem(IMonoSystem monoSystem, Type bindToType)
        {
            if (monoSystem == null || bindToType == null)
            {
                throw new Exception($"{nameof(monoSystem)} cannot be null");
            }
            
            if (monoSystems.ContainsKey(bindToType))
            {
                throw new Exception($"Mono System of type {bindToType} already exists");
            }

            monoSystems[bindToType] = monoSystem;
        }
        
        public void RemoveMonoSystem<TMonoSystem, TBindTo>(TMonoSystem monoSystem)
            where TMonoSystem : TBindTo, IMonoSystem
        {
            if (monoSystem == null)
            {
                throw new Exception($"{nameof(monoSystem)} cannot be null");
            }
            
            var monoSystemType = typeof(TBindTo);
            
            if(!monoSystems.ContainsKey(monoSystemType))
            {
                throw new Exception($"Mono System {monoSystemType} not found. Cannot remove it.");
            }
            
            monoSystems.Remove(monoSystemType);
        }
        
        public void RemoveMonoSystem(IMonoSystem monoSystem, Type bindToType)
        {
            if (monoSystem == null || bindToType == null)
            {
                throw new Exception($"{nameof(monoSystem)} cannot be null");
            }
            
            if (!monoSystems.ContainsKey(bindToType))
            {
                throw new Exception($"State Machine {bindToType} not found. Cannot remove it.");
            }

            monoSystems.Remove(bindToType);
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
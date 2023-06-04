using System;
using System.Collections.Generic;

namespace Akashic.Core
{
    internal sealed class ServiceManager
    {
        private readonly IDictionary<Type, IService> services =
            new Dictionary<Type, IService>();

        public void AddService<TService, TBindTo>(TService service)
            where TService : TBindTo, IService
        {
            if (service == null)
            {
                throw new Exception($"{nameof(service)} cannot be null");
            }

            var serviceType = typeof(TBindTo);
            services[serviceType] = service;
        }

        public TService GetService<TService>()
        {
            var serviceType = typeof(TService);
            if (services.TryGetValue(serviceType, out var service))
            {
                return (TService)service;
            }

            throw new Exception($"Service {serviceType} does not exist");
        }
    }
}
using System;
using System.Collections.Generic;

namespace Services
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> _serviceContainer;

        public ServiceLocator()
        {
            _serviceContainer = new Dictionary<Type, object>();
        }

        public TService GetService<TService>()
        {
            try
            {
                return (TService)_serviceContainer[typeof(TService)];
            }
            catch
            {
                throw new ServiceInitializationException($"Requested service does not exist: {typeof(TService)}");
            }
        }

        public void InitializeService<TService>(TService service) => _serviceContainer.TryAdd(typeof(TService), service);
    }
}
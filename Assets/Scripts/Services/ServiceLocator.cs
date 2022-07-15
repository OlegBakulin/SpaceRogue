using System;
using System.Collections.Generic;

namespace Services
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> ServiceContainer;

        static ServiceLocator()
        {
            ServiceContainer = new Dictionary<Type, object>();
        }

        public static TService GetService<TService>()
        {
            try
            {
                return (TService)ServiceContainer[typeof(TService)];
            }
            catch
            {
                throw new ServiceInitializationException($"Requested service does not exist: {typeof(TService)}");
            }
        }

        public static void InitializeService<TService>(TService service) => ServiceContainer.TryAdd(typeof(TService), service);
    }
}
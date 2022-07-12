using System;

namespace Services
{
    public class ServiceInitializationException : Exception
    {
        public ServiceInitializationException(string message) : base(message)
        {
        }
    }
}
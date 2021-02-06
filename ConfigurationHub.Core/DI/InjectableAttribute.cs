using System;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationHub.Core.DI
{
    public abstract class InjectableAttribute : Attribute
    {
        protected InjectableAttribute(ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            Lifetime = lifetime;
        }

        public ServiceLifetime Lifetime { get; }
    }
}

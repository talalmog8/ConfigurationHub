using System;
using System.Linq;
using ConfigurationHub.General;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace ConfigurationHub.Core.DI
{
    public static class AssemblyScanningExtensions
    {
        public static IImplementationTypeSelector InjectableAttributes(this IImplementationTypeSelector selector)
        {
            Enum.GetValues(typeof(ServiceLifetime))
                .Cast<ServiceLifetime>()
                .ForEach(item => selector = selector.InjectableAttribute(item));

            return selector;
        }

        public static IImplementationTypeSelector InjectableAttribute(this IImplementationTypeSelector selector, ServiceLifetime lifeTime)
        {
            return selector.AddClasses(c => c.WithAttribute<InjectableAttribute>(i => i.Lifetime == lifeTime))
                .AsImplementedInterfaces()
                .WithLifetime(lifeTime);
        }
    }
}
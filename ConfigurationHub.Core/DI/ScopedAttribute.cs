using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationHub.Core.DI
{
    public class ScopedAttribute : InjectableAttribute
    {
        public ScopedAttribute() : base(ServiceLifetime.Scoped) { }
    }
}
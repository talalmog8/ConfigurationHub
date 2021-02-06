using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationHub.Core.DI
{
    public class SingleTonAttribute : InjectableAttribute
    {
        public SingleTonAttribute() : base(ServiceLifetime.Singleton) { }
    }
}
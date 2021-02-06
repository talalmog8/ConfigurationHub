using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationHub.Core.DI
{
    public class TransientAttribute : InjectableAttribute
    {
        public TransientAttribute () : base(ServiceLifetime.Transient) { }
    }
}
using ConfigurationHub.Domain.Auth;

namespace ConfigurationHub.Domain
{
    public class ConfigDto
    {
        public User Author { get; set; }
        public ConfigContent ConfigContent { get; set; }
        public Microservice Microservice { get; set; }
    }
}

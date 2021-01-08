using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.Auth
{
    public class AuthenticateDto
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}
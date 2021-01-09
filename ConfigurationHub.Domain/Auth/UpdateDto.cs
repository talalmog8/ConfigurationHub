using System.ComponentModel.DataAnnotations;

namespace ConfigurationHub.Domain.Auth
{
    public class UpdateDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Email { get; set; }
    }
}

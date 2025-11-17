using System.ComponentModel.DataAnnotations;

namespace Equilibrium.Api.DTOs
{
    public class CreateUserDto
    {
        [Required] public string FullName { get; set; }
        [Required] public string Email { get; set; }
        public string PreferredMode { get; set; }
    }
}

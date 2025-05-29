using System.ComponentModel.DataAnnotations;

namespace Application.Services.User.Payload
{
    public class UserPayload
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CPF { get; set; }
    }
}

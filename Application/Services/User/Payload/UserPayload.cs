using System.ComponentModel.DataAnnotations;

namespace Application.Services.User.Payload
{
    public class UserPayload
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [MinLength(1, ErrorMessage = "O Nome não pode estar vazio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [MinLength(1, ErrorMessage = "O Email não pode estar vazio.")]
        [EmailAddress(ErrorMessage = "O Email não é válido.")] 
        public string Email { get; set; }
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [MinLength(1, ErrorMessage = "O CPF não pode estar vazio.")]
        public string CPF { get; set; }
    }
}

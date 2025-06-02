using Domain.Entities;

namespace Application.Repository
{
    public interface IUserRepository
    {
        Task<bool> CreateAccountAsync(UserEntity user);

        Task<bool> VerifyUserExistsByDocument(string cpf);
        
        Task<bool> VerifyUserExistsByEmail(string email);

        Task<UserEntity?> IdentificationAsync(string cpfOrEmail);
    }
}

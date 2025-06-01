using Domain.Entities;

namespace Application.Repository
{
    public interface IUserRepository
    {
        Task<bool> CreateAccountAsync(UserEntity user);

        Task<bool> VerifyUserExistsByDocument(string document);

        Task<UserEntity?> IdentificationAsync(string document);
    }
}

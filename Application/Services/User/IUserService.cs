using Application.Services.User.Payload;

namespace Application.Services.User
{
    public interface IUserService
    {
        Task<bool> CreateAccountAsync(UserPayload user);
        Task<UserPayload?> IdentificationAsync(string cpf);
    }
}

using Application.Repository;
using Application.Services.User.Mappers;
using Application.Services.User.Payload;
using Application.Services.Utils;

namespace Application.Services.User
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<bool> CreateAccountAsync(UserPayload user)
        {
            bool isCpf = CPFUtils.IsCpf(user.CPF);
            if(!isCpf)
                throw new ArgumentException($"CPF invalido!");

            var userAlreadyExists = await userRepository.VerifyUserExistsByDocument(user.CPF);
            if (userAlreadyExists)
                throw new ArgumentException("Usuário já existe!");

            return await userRepository.CreateAccountAsync(UserMapper.ToEntity(user));
        }

        public async Task<UserPayload?> IdentificationAsync(string cpf)
        {
            var userEntity = await userRepository.IdentificationAsync(cpf);
            if (userEntity == null)
                return null;

            return UserMapper.ToPayload(userEntity);
        }
    }
}

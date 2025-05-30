using Application.Repository;
using Application.Services.User.Mappers;
using Application.Services.User.Payload;

namespace Application.Services.User
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<bool> CreateAccountAsync(UserPayload user)
        {
            var userAlreadyExists = await userRepository.VerifyUserExistsByDocument(user.CPF);
            if (userAlreadyExists)
                return false;

            return await userRepository.CreateAccountAsync(UserMapper.ToEntity(user));
        }

        public async Task<UserPayload?> IdentificationAsync(UserPayload user)
        {
            var userEntity = await userRepository.IdentificationAsync(UserMapper.ToEntity(user));
            if (userEntity == null)
                return null;

            return UserMapper.ToPayload(userEntity);
        }
    }
}

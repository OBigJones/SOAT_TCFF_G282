using Application.Repository;
using Application.Services.User.Payload;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAccountAsync(UserPayload user)
        {
            var userAlreadyExists = await _userRepository.VerifyUserExistsByDocument(user.CPF);
            if (userAlreadyExists)
                return false;

            return await _userRepository.CreateAccountAsync(_mapper.Map<UserEntity>(user));
        }

        public async Task<UserPayload> IdentificationAsync(UserPayload user)
        {
            var userEntity = await _userRepository.IdentificationAsync(_mapper.Map<UserEntity>(user));
            if (userEntity == null)
                return null;

            return _mapper.Map<UserPayload>(userEntity);
        }
    }
}

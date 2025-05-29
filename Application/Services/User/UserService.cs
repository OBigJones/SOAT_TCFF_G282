using Application.Repository;
using Application.Services.User.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var userAlreadyExists = await _userRepository.VerifyUserExistsByDocument(user.Document);
            if (userAlreadyExists)
                return false;

            return await _userRepository.CreateAccountAsync(_mapper.Map<User>(clienteEntity));
        }
    }
}

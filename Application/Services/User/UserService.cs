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
        // private IUserRepository _userRepository;
        public UserService()//IUserRepository userRepository)
        {
            // _userRepository = userRepository;
        }

        public async Task<bool> CreateAccountAsync(UserPayload user)
        {
            var userAlreadyExists = true;// await _userRepository.VerifyUserExistsByDocument(user.Document);
            if (userAlreadyExists)
                return false;

            return true;// await _userRepository.CreateAccountAsync(user);
        }
    }
}

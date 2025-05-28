using Application.Services.User.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IUserRepository
    {
        Task<bool> CreateAccountAsync(UserPayload user);

        Task<bool> VerifyUserExistsByDocument(string document);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repository
{
    public interface IUserRepository
    {
        Task<bool> CreateAccountAsync(User user);

        Task<bool> VerifyUserExistsByDocument(string document);
    }
}

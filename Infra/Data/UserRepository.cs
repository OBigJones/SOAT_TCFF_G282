using Application.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context) => _context = context;

        public async Task<bool> CreateAccountAsync(UserEntity user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> VerifyUserExistsByDocument(string document)
        {
            return await _context.Users.AnyAsync(u => u.CPF == document);
        }

        public async Task<UserEntity> IdentificationAsync(UserEntity user)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.CPF == user.CPF);
        }
    }
}

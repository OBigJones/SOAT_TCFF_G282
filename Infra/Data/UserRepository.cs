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

        public async Task<bool> VerifyUserExistsByDocument(string cpf)
        {
            return await _context.Users.AnyAsync(u => u.CPF == cpf);
        }
        
        public async Task<bool> VerifyUserExistsByEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<UserEntity?> IdentificationAsync(string cpfOrEmail)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.CPF == cpfOrEmail);
            if (userEntity != null)
                return userEntity;
            
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == cpfOrEmail);
        }
    }
}

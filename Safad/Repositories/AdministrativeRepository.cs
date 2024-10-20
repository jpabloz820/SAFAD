using Microsoft.EntityFrameworkCore;
using Safad.Data;
using Safad.Interfaces;
using Safad.Models;

namespace Safad.Repositories
{
    public class UserAdministrativeRepository : Repository<UserAdministrative>, IUserAdministrativeRepositor
    {
        private readonly SafadDBContext _context;

        public UserAdministrativeRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserAdministrative> GetUserByIdUserAsync(int userId)
        {
            return await _context.UserAdministratives.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
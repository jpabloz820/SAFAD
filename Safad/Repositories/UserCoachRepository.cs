using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;

namespace Safad.Repositories
{
    public class UserCoachRepository : Repository<UserCoach>, IUserCoachRepository
    {
        private readonly SafadDBContext _context;

        public UserCoachRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Método genérico para obtener todos el userCoach por el userId
        /// </summary>
        /// <returns>Registro con la información encontrada</returns>
        public async Task<UserCoach> GetByUserId(int userId)
        {
            return await _context.UserCoaches
                .FirstOrDefaultAsync(uc => uc.UserId == userId);
        }
    }
}

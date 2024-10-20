using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;



namespace Safad.Repositories
{
    public class UserAthleteRepository : Repository<User_Athlete>, IUserAthleteRepository
    {
        private readonly SafadDBContext _context;

        public UserAthleteRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Método genérico para obtener  el user Athlete por el userId
        /// </summary>
        /// <returns>Registro con la información encontrada</returns>
        public async Task<User_Athlete> GetByUserId(int userId)
        {
            return await _context.UserAthletes
                .FirstOrDefaultAsync(uc => uc.UserId == userId);
        }
    }
}

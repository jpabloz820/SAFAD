using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;

namespace Safad.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly SafadDBContext _context;

        public UserRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un usuario por su correo electrónico de la base de datos
        /// </summary>
        /// <param name="email">Correo electrónico del usuario a buscar</param>
        /// <returns>Objeto User si se encuentra, de lo contrario null</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
        }
    }
}

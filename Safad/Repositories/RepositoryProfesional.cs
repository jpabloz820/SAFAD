using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;

namespace Safad.Repositories
{

    public class RepositoryProfesional : Repository<Profesional>, IProfesionalRepository
    {
        private readonly SafadDBContext _context;

        public  RepositoryProfesional(SafadDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Profesional> GetByUserId(int userId)
        {
            return await _context.Profesional
                .FirstOrDefaultAsync(uc => uc.UserId == userId);
        }
    }
}

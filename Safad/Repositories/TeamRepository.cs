using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;

namespace Safad.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly SafadDBContext _context;

        public TeamRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un equipo por el Id de Entrenador
        /// </summary>
        /// <param name="UserCoachId">Id del Entrenador para buscar</param>
        public async Task<Team> GetTeamByUserCoachId(int UserCoachId)
        {
            return await _context.Teams
                .FirstOrDefaultAsync(u => u.UserCoachId == UserCoachId);
        }
    }
}

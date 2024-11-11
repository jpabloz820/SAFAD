using Microsoft.EntityFrameworkCore;
using Safad.Data;
using Safad.Interfaces;
using Safad.Models;

namespace Safad.Repositories
{
    public class TeamUserAthleteRepository : Repository<TeamUserAthlete>, ITeamUserAthleteRepository
    {
        private readonly SafadDBContext _context;

        public TeamUserAthleteRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los atletas asociados a un equipo específico por su TeamId.
        /// </summary>
        /// <param name="teamId">Id del Equipo para buscar</param>
        /// <returns>Una lista de relaciones TeamUserAthlete asociadas al TeamId</returns>
        public async Task<List<TeamUserAthlete>> GetTeamUserAthletesByTeamId(int teamId)
        {
            return await _context.TeamUserAthletes
                .Where(u => u.TeamId == teamId)
                .Include(u => u.User_Athlete)
                .ThenInclude(a => a.User)
                .ToListAsync();
        }
    }
}

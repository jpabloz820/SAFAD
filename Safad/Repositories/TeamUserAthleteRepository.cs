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
    }
}

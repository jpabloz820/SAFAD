using Safad.Data;
using Safad.Interfaces;
using Safad.Models;

namespace Safad.Repositories
{
    public class TeamProfessionalRepository : Repository<TeamProfessional>, ITeamProfessionalRepository
    {
        private readonly SafadDBContext _context;

        public TeamProfessionalRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

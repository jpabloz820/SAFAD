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
    }
}

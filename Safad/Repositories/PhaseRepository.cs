using Safad.Data;
using Safad.Interfaces;
using Safad.Models;

namespace Safad.Repositories
{
    public class PhaseRepository : Repository<Phase>, IPhaseRepository
    {
        private readonly SafadDBContext _context;

        public PhaseRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

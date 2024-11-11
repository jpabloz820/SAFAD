using Safad.Data;
using Safad.Interfaces;
using Safad.Models;

namespace Safad.Repositories
{
    public class DivisionRepository : Repository<Division>, IDivisionRepository
    {
        private readonly SafadDBContext _context;

        public DivisionRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

using Safad.Data;
using Safad.Interfaces;
using Safad.Models;

namespace Safad.Repositories
{
    public class GoalIndicatorRepository : Repository<GoalIndicator>, IGoalIndicatorRepository
    {
        private readonly SafadDBContext _context;

        public GoalIndicatorRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

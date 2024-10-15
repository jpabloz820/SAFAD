using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;

namespace Safad.Repositories
{
    public class UserCoachRepository : Repository<UserCoach>, IUserCoachRepository
    {
        private readonly SafadDBContext _context;

        public UserCoachRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

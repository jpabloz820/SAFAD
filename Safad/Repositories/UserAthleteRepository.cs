using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;



namespace Safad.Repositories
{
    public class UserAthleteRepository : Repository<User_Athlete>, IUserAthleteRepository
    {
        private readonly SafadDBContext _context;

        public UserAthleteRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;

namespace Safad.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly SafadDBContext _context;
        public RoleRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;

namespace Safad.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly SafadDBContext _context;

        public CategoryRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

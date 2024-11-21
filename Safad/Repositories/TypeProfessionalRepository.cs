using Microsoft.EntityFrameworkCore;
using Safad.Interfaces;
using Safad.Models;
using Safad.Data;


namespace Safad.Repositories
{
    public class TypeProfessionalRepository : Repository<TypeProfessional>, ITypeProfessionalRepository
    {
        private readonly SafadDBContext _context;

        public TypeProfessionalRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

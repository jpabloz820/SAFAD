using Safad.Data;
using Safad.Interfaces;
using Safad.Models;

namespace Safad.Repositories
{
    public class MetricRepository : Repository<Metric>, IMetricRepository
    {
        private readonly SafadDBContext _context;

        public MetricRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }
    }
}

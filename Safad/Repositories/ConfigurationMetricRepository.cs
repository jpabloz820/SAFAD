using Safad.Interfaces;
using Safad.Models;
using Safad.Data;
using Microsoft.EntityFrameworkCore;

namespace Safad.Repositories
{
    public class ConfigurationMetricRepository : Repository<ConfigurationMetric>, IConfigurationMetricRepository
    {
        private readonly SafadDBContext _context;

        public ConfigurationMetricRepository(SafadDBContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la configuració de métricas por el Id de Phase y Category
        /// </summary>
        /// <param name="phaseId">Id de la fase para buscar</param>
        /// <param name="categoryId">Id de la categoria para buscar</param>
        /// <returns>IEnumerable<ConfigurationMetric> Lista de métricas</returns>
        public async Task<IEnumerable<ConfigurationMetric>> GetByPhaseAndCategory(int phaseId, int categoryId)
        {
            return await _context.ConfigurationMetrics
                                 .Include(cm => cm.Metric)
                                 .Where(cm => cm.PhaseId == phaseId && cm.CategoryId == categoryId)
                                 .ToListAsync();
        }
    }
}

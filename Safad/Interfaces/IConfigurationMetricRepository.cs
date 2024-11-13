using Safad.Models;

namespace Safad.Interfaces
{
    public interface IConfigurationMetricRepository : IRepository<ConfigurationMetric>
    {
        Task<IEnumerable<ConfigurationMetric>> GetByPhaseAndCategory(int phaseId, int categoryId);
    }
}

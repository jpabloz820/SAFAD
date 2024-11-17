using Microsoft.EntityFrameworkCore;
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
        /// <summary>
        /// Método genérico para obtener los indicadores según la fase, el deportista y la metrica
        /// </summary>
        /// <returns>Registro con la información encontrada</returns>
        public async Task<List<GoalIndicator>> GetByPhaseUserMetric(int phaseId, int userAthleteId, int metricId, int limit)
        {
            return await _context.GoalIndicators
                .Join(
                    _context.ConfigurationMetrics,
                    gi => gi.MetricId,
                    cm => cm.MetricId,
                    (gi, cm) => new { gi, cm })
                .Where(joined =>
                    joined.cm.PhaseId == phaseId &&
                    joined.gi.UserAthleteId == userAthleteId &&
                    joined.gi.MetricId == metricId)
                .OrderByDescending(joined => joined.gi.GoalIndicatorId)
                .Select(joined => joined.gi)
                .Take(limit)
                .ToListAsync();
        }
    }
}

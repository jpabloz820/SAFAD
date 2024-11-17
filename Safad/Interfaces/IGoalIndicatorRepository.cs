using Safad.Models;

namespace Safad.Interfaces
{
    public interface IGoalIndicatorRepository : IRepository<GoalIndicator>
    {
        Task<List<GoalIndicator>> GetByPhaseUserMetric(int phaseId, int userAthleteId, int metricId, int limit);
    }
}

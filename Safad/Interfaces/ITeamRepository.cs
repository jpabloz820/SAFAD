using Safad.Models;

namespace Safad.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<Team> GetTeamByUserCoachId(int UserCoachId);
    }
}

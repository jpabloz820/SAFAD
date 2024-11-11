using Safad.Models;

namespace Safad.Interfaces
{
    public interface ITeamUserAthleteRepository : IRepository<TeamUserAthlete>
    {
        Task<List<TeamUserAthlete>> GetTeamUserAthletesByTeamId(int teamId);
    }
}

using Safad.Models;

namespace Safad.Interfaces
{
    public interface IUserAthleteRepository : IRepository<User_Athlete>
    {
        Task<User_Athlete> GetByUserId(int userId);
    }
}

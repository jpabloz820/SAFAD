using Safad.Models;

namespace Safad.Interfaces
{
    public interface IUserCoachRepository : IRepository<UserCoach>
    {
        Task<UserCoach> GetByUserId(int userId);
    }
}

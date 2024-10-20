using Safad.Models;

namespace Safad.Interfaces
{
    public interface IUserAdministrativeRepositor : IRepository<UserAdministrative>
    {
        Task<UserAdministrative> GetUserByIdUserAsync(int userId);
    }
}

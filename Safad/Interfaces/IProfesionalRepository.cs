using Microsoft.EntityFrameworkCore;
using Safad.Data;
using Safad.Models;

namespace Safad.Interfaces
{
    public interface IProfesionalRepository : IRepository<Profesional>
    {
        Task<Profesional> GetByUserId(int userId);

    }
}

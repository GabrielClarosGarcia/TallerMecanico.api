using TallerMecanico.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TallerMecanico.Core.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<Client> GetByEmailAsync(string email);
        Task<IEnumerable<Client>> GetByNameAsync(string name);
    }
}

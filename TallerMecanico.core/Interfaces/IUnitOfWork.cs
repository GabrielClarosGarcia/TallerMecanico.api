using System.Threading.Tasks;

namespace TallerMecanico.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }
        IVehicleRepository Vehicles { get; }
        IServiceRepository Services { get; }

        Task<int> SaveChangesAsync();
    }
}

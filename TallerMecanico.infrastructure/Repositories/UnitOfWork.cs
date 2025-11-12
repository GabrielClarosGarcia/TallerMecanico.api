using System.Threading.Tasks;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Data;

namespace TallerMecanico.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkshopContext _context;

        public IClientRepository Clients { get; }
        public IVehicleRepository Vehicles { get; }
        public IServiceRepository Services { get; }

        public UnitOfWork(
            WorkshopContext context,
            IClientRepository clientRepository,
            IVehicleRepository vehicleRepository,
            IServiceRepository serviceRepository)
        {
            _context = context;
            Clients = clientRepository;
            Vehicles = vehicleRepository;
            Services = serviceRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

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


        private readonly ISecurityRepository _securityRepository;
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
        public ISecurityRepository SecurityRepository =>
           _securityRepository ?? new SecurityRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

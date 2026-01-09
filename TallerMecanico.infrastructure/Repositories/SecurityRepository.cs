using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Entities;
using TallerMecanico.Infrastructure.Data;
using TallerMecanico.Core.Interfaces;

namespace TallerMecanico.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(WorkshopContext context) : base(context) { }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _context.Securities
                .FirstOrDefaultAsync(s => s.Login == login.User);
        }


        public async Task AddAsync(Security security)
        {
            await _context.Securities.AddAsync(security); 
        }

        // Guardar cambios en la base de datos
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}

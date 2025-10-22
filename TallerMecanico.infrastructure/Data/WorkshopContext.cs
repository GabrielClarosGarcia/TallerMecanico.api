using Microsoft.EntityFrameworkCore;
using TallerMecanico.infrastructure.Entities;

namespace TallerMecanico.infrastructure.Data;

public partial class WorkshopContext : DbContext
{
    public WorkshopContext() { }

    public WorkshopContext(DbContextOptions<WorkshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<client> clients { get; set; } = null!;
    public virtual DbSet<service> services { get; set; } = null!;
    public virtual DbSet<user> users { get; set; } = null!;
    public virtual DbSet<vehicle> vehicles { get; set; } = null!;
}

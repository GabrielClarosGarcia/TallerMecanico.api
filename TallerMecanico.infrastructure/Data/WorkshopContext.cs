using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Infrastructure.Data
{
    public partial class WorkshopContext : DbContext
    {
        public WorkshopContext() { }

        public WorkshopContext(DbContextOptions<WorkshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<WorkshopService> Services { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("clients");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Vehicle>().ToTable("vehicles");
            modelBuilder.Entity<WorkshopService>().ToTable("services");

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Client)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.IdClient)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<WorkshopService>()
                 .HasOne(ws => ws.Vehicle)
                 .WithMany(v => v.Services)
                 .HasForeignKey(ws => ws.IdVehicle)
                 .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Infrastructure.Data
{
    public partial class WorkshopContext : DbContext
    {
        public WorkshopContext(DbContextOptions<WorkshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Security> Securities { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=workshop_db;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para Client
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient).HasName("PRIMARY");
                entity.ToTable("clients");
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            // Configuración para Security
            modelBuilder.Entity<Security>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("security");
                entity.Property(e => e.Login).HasMaxLength(50);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Password).HasMaxLength(200);
                entity.Property(e => e.Role).HasMaxLength(15);
            });

            // Configuración para Service
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.IdService).HasName("PRIMARY");
                entity.ToTable("services");

                // Relación con Vehicle
                entity.HasOne(d => d.Vehicle)  // Relación de un Service con un Vehicle
                    .WithMany(v => v.Services)  // Un Vehicle puede tener múltiples Services
                    .HasForeignKey(d => d.IdVehicle)  // La FK en Service es IdVehicle
                    .OnDelete(DeleteBehavior.ClientSetNull)  // Si se elimina un Vehicle, los Services se mantienen sin eliminación en cascada
                    .HasConstraintName("services_ibfk_1");

                entity.Property(e => e.Description).HasMaxLength(200);
            });

            // Configuración para Vehicle
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.IdVehicle).HasName("PRIMARY");
                entity.ToTable("vehicles");

                entity.HasIndex(e => e.IdClient, "IdClient");

                entity.Property(e => e.Brand).HasMaxLength(50);
                entity.Property(e => e.Model).HasMaxLength(50);
                entity.Property(e => e.Plate).HasMaxLength(20);

                // Relación con Client
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vehicles_ibfk_1");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

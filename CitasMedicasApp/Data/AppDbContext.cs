using Microsoft.EntityFrameworkCore;
using CitasMedicasApp.Models;

namespace CitasMedicasApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol { RolId = 1, Nombre = "Cliente" },
                new Rol { RolId = 2, Nombre = "Administrador" },
                new Rol { RolId = 3, Nombre = "Doctor" }
            );

            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.NumeroCedula)
                .IsUnique();

            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Usuario)
                .WithMany()
                .HasForeignKey(m => m.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Departamento)
                .WithMany()
                .HasForeignKey(m => m.IdDepartamento)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Departamento)
                .WithMany()
                .HasForeignKey(s => s.DepartamentoAsociado)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

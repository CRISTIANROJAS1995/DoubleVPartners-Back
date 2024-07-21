using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class ApplicationDbContext : DbContext, IDisposable
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
            
        }
        public DbSet<AsignacionTarea> AsignacionTarea { get; set; }
        public DbSet<EstadoTarea> EstadoTarea { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Tarea> Tarea { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}

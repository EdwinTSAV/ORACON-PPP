using Microsoft.EntityFrameworkCore;
using Oracon.Models.Class;
using Oracon.Models.Maps;

namespace Oracon.Models
{
    public class Oracon_Context : DbContext
    {
        public Oracon_Context(DbContextOptions<Oracon_Context> options)
            : base(options) { }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new RolesMap());
        }
    }
}

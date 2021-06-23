using Microsoft.EntityFrameworkCore;
using Oracon.Models;

namespace Oracon.Maps
{
    public class Oracon_Context : DbContext
    {
        public Oracon_Context(DbContextOptions<Oracon_Context> options)
            : base(options) { }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new RolesMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new CursoMap());
        }
    }
}

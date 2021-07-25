using Microsoft.EntityFrameworkCore;
using Oracon.Models;

namespace Oracon.Maps
{
    public class Oracon_Context : DbContext
    {
        public Oracon_Context(DbContextOptions<Oracon_Context> o) : base(o) { }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Favoritos> Favoritos { get; set; }
        public DbSet<CursoUsuario> CursoUsuario { get; set; }
        public DbSet<Aprendizaje> Aprendizajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new RolesMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new FavoritosMap());
            modelBuilder.ApplyConfiguration(new CursoUsuarioMap());
            modelBuilder.ApplyConfiguration(new AprendizajeMap());
        }
    }
}

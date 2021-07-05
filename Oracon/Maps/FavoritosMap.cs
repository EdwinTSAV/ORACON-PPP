using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oracon.Models;

namespace Oracon.Maps
{
    public class FavoritosMap : IEntityTypeConfiguration<Favoritos>
    {
        public void Configure(EntityTypeBuilder<Favoritos> builder)
        {
            builder.ToTable("Favoritos");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Cursos).
                WithMany().
                HasForeignKey(o => o.IdCurso);

            builder.HasOne(o => o.Usuarios).
                WithMany().
                HasForeignKey(o => o.IdUser);
        }
    }
}

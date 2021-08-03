using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oracon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oracon.Maps
{
    public class ComentarioCursoMap : IEntityTypeConfiguration<ComentarioCurso>
    {
        public void Configure(EntityTypeBuilder<ComentarioCurso> builder)
        {
            builder.ToTable("ComentarioCurso");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Usuario).
                WithMany().
                HasForeignKey(o => o.IdUsuario);
        }
    }
}

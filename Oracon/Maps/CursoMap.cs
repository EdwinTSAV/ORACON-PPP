﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oracon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oracon.Maps
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Docente).
                WithMany().
                HasForeignKey(o => o.IdDocente);

            builder.HasOne(o => o.Categoria).
                WithMany().
                HasForeignKey(o => o.IdCategoria);
        }
    }
}

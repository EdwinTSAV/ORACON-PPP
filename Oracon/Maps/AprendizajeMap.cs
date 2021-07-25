using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oracon.Models;
using System;

namespace Oracon.Maps
{
    public class AprendizajeMap : IEntityTypeConfiguration<Aprendizaje>
    {
        public void Configure(EntityTypeBuilder<Aprendizaje> builder)
        {
            builder.ToTable("Aprendizaje");
            builder.HasKey(o => o.Id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oracon.Models;
using System;

namespace Oracon.Maps
{
    public class RequisitosMap : IEntityTypeConfiguration<Requisitos>
    {
        public void Configure(EntityTypeBuilder<Requisitos> builder)
        {
            builder.ToTable("Requisitos");
            builder.HasKey(o => o.Id);
        }
    }
}

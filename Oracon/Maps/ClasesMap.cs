using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oracon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oracon.Maps
{
    public class ClasesMap : IEntityTypeConfiguration<Clases>
    {
        public void Configure(EntityTypeBuilder<Clases> builder)
        {
            builder.ToTable("Clases");
            builder.HasKey(o => o.Id);
        }
    }
}

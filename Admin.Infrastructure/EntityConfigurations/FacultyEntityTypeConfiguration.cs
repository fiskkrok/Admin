using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.AggregateModels.FacultyAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Admin.Infrastructure.EntityConfigurations;

public class FacultyEntityTypeConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> entity)
    {
        entity.HasKey(e => e.FacultyId).HasName("PK__Faculty__306F636E67296B45");

        entity.Property(e => e.FacultyId).ValueGeneratedNever();
    }
}
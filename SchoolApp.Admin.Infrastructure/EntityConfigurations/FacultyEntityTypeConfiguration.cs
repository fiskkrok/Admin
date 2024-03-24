using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Application.AggregateModels.FacultyAggregate;


namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class FacultyEntityTypeConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> entity)
    {
        entity.HasKey(b => b.Id.GetHashCode());

        entity.Property(b => b.Id)
            .ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.HasAlternateKey(e => e.FacultyId);

        entity.Property(e => e.FacultyId).ValueGeneratedNever();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Application.AggregateModels.CourseAggregate;


namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> entity)
    {
        entity.HasKey(b => b.Id.GetHashCode());

        entity.Property(b => b.Id)
            .ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.HasAlternateKey(e => e.CourseId);
        entity.Property(e => e.CourseId).ValueGeneratedNever();
    }
}
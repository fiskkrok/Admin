using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.AggregateModels.CourseAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Admin.Infrastructure.EntityConfigurations;

public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> courseConfiguration)
    {
        courseConfiguration.HasKey(e => e.CourseId);
        courseConfiguration.Property(e => e.CourseId).ValueGeneratedNever();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.AggregateModels.EnrollmentAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Admin.Infrastructure.EntityConfigurations;

public class EnrollmentEntityTypeConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> entity)
    {
        entity.HasKey(e => e.EnrollmentId);

        entity.Property(e => e.EnrollmentId).ValueGeneratedNever();

        entity.HasOne(d => d.Course).WithMany(p => p.Enrollments);

        entity.HasOne(d => d.Student).WithMany(p => p.Enrollments);

    }
}
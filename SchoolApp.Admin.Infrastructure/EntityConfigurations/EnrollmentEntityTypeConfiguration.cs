using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Application.AggregateModels.EnrollmentAggregate;


namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class EnrollmentEntityTypeConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> entity)
    {
        entity.HasKey(b => b.Id.GetHashCode());

        entity.Property(b => b.Id)
            .ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.HasAlternateKey(e => e.EnrollmentId);

        entity.Property(e => e.EnrollmentId).ValueGeneratedNever();

        entity.HasOne(d => d.Course).WithMany(p => p.Enrollments);

        entity.HasOne(d => d.Student).WithMany(p => p.Enrollments);

    }
}
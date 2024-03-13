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
        entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F6877FB5B9B0E72");

        entity.Property(e => e.EnrollmentId).ValueGeneratedNever();

        entity.HasOne(d => d.Course).WithMany(p => p.Enrollments).HasConstraintName("FK__Enrollmen__Cours__4E88ABD4");

        entity.HasOne(d => d.Student).WithMany(p => p.Enrollments).HasConstraintName("FK__Enrollmen__Stude__4D94879B");

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.AggregateModels.CourseAssignmentAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Admin.Infrastructure.EntityConfigurations;

public class CourseAssignmentEntityTypeConfiguration : IEntityTypeConfiguration<CourseAssignment>
{
    public void Configure(EntityTypeBuilder<CourseAssignment> entity)
    {
        entity.HasKey(e => e.AssignmentId).HasName("PK__CourseAs__32499E571698A95E");

        entity.Property(e => e.AssignmentId).ValueGeneratedNever();

        entity.HasOne(d => d.Course).WithMany(p => p.CourseAssignments).HasConstraintName("FK__CourseAss__Cours__5441852A");

        entity.HasOne(d => d.Faculty).WithMany(p => p.CourseAssignments).HasConstraintName("FK__CourseAss__Facul__534D60F1");

    }
}
﻿
namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class CourseAssignmentEntityTypeConfiguration : IEntityTypeConfiguration<CourseAssignment>
{
    public void Configure(EntityTypeBuilder<CourseAssignment> entity)
    {

        entity.HasKey(b => b.Id);

        entity.Property(b => b.Id)
            .ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.HasAlternateKey(e => e.AssignmentId);

        entity.Property(e => e.AssignmentId).ValueGeneratedNever();

        entity.HasOne(d => d.Course).WithMany(p => p.CourseAssignments);

        entity.HasOne(d => d.Faculty).WithMany(p => p.CourseAssignments);

    }
}
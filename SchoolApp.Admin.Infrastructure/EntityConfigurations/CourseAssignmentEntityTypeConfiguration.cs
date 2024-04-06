namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class CourseAssignmentEntityTypeConfiguration : IEntityTypeConfiguration<CourseAssignment>
{
    public void Configure(EntityTypeBuilder<CourseAssignment> entity)
    {
        entity.HasKey(b => b.AssignmentId); // Set AssignmentId as the primary key
        entity.HasIndex(e => e.Id); // Create an index on the Id property
        entity.Property(b => b.Id).ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values

        // Establishing realistic relationships to Course and Faculty
        entity.HasOne(d => d.Course).WithMany(p => p.CourseAssignments).HasForeignKey(d => d.CourseId);
        entity.HasOne(d => d.Faculty).WithMany(p => p.CourseAssignments).HasForeignKey(d => d.FacultyId);
    }
}